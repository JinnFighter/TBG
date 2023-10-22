using System.Collections.Generic;
using System.Linq;
using Logic.Actions;
using Logic.Actions.ActionLogic;
using Logic.AI;
using Logic.CharacterQueue;
using Logic.Characters;
using Logic.Config;
using Logic.TurnSteps;
using UnityEngine;
using UnityEngine.Events;
using CharacterInfo = Logic.Characters.CharacterInfo;

namespace Logic.BattleService
{
    public class BattleService : IBattleService
    {
        private readonly IAiActionSubmitter _aiActionSubmitter;
        private readonly AttackAbilityConfig _attackAbilityConfig;
        private readonly ICharacterQueue _characterQueue;

        private readonly GameStateMachine _gameStateMachine = new();

        public BattleService(
            AttackAbilityConfig attackAbilityConfig)
        {
            CharactersContainer = new CharactersContainer();
            _characterQueue = new CharacterQueue.CharacterQueue();
            ActionProcessor = new ActionProcessor();
            ActionSubmitter = new ActionSubmitter();
            _aiActionSubmitter = new AiActionSubmitter(this, ActionSubmitter, _characterQueue, attackAbilityConfig);
            _attackAbilityConfig = attackAbilityConfig;
        }

        public IActionSubmitter ActionSubmitter { get; }
        public IActionProcessor ActionProcessor { get; }
        public CharactersContainer CharactersContainer { get; }

        public bool IsBattleStarted { get; private set; }
        public bool IsBattleFinished { get; private set; }
        public UnityEvent<ETurnStep> OnTurnStepEnter { get; } = new();

        public void Init()
        {
        }

        public void Terminate()
        {
            _gameStateMachine.OnTurnEnd.RemoveListener(HandleTurnEnded);
            _gameStateMachine.OnStateEnter.RemoveListener(OnTurnStepEnter.Invoke);

            _aiActionSubmitter.Terminate();
            _characterQueue.Terminate(); 
            ActionProcessor.Terminate();
            CharactersContainer.Terminate();
            _gameStateMachine.Terminate();

            IsBattleStarted = false;
            IsBattleFinished = false;
        }

        public void StartBattle(List<CharacterInfo> characterInfos)
        {
            CharactersContainer.Init(characterInfos);
            _characterQueue.Init(CharactersContainer.Characters.Select(character => character.Value.CharacterData.Id));

            _gameStateMachine.Init(new List<ITurnStep>
            {
                new SelectTeamTurnStep(_characterQueue),
                new AwaitingInputTurnStep(ActionSubmitter),
                new ProcessActionsTurnStep(ActionProcessor),
            });

            _gameStateMachine.OnTurnEnd.AddListener(HandleTurnEnded);
            _gameStateMachine.OnStateEnter.AddListener(OnTurnStepEnter.Invoke);

            ActionProcessor.Init(new List<IActionLogic>
            {
                new DamageActionLogic(CharactersContainer, _attackAbilityConfig)
            });

            _aiActionSubmitter.Init();

            _gameStateMachine.GoToNextState();

            IsBattleStarted = true;
        }

        private void HandleTurnEnded()
        {
            if (CharactersContainer.CharacterTeams[ECharacterTeam.Player]
                .All(character => character.CharacterStats.Health <= 0))
            {
                Debug.Log("Game over => enemy wins!");
                IsBattleFinished = true;
            }

            else if (CharactersContainer.CharacterTeams[ECharacterTeam.Enemy]
                     .All(character => character.CharacterStats.Health <= 0))
            {
                Debug.Log("Game over => player wins!");
                IsBattleFinished = true;
            }

            else
            {
                _gameStateMachine.GoToNextState();
            }
        }
    }
}