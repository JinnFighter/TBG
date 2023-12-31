﻿using System.Collections.Generic;
using System.Linq;
using Logic.Actions;
using Logic.Actions.ActionLogic;
using Logic.AI;
using Logic.CharacterQueue;
using Logic.Characters;
using Logic.TurnSteps;
using UnityEngine;
using UnityEngine.Events;
using CharacterInfo = Logic.Characters.CharacterInfo;

namespace Logic.BattleService
{
    public class BattleService : IBattleService
    {
        private readonly IActionProcessor _actionProcessor;
        private readonly IAiActionSubmitter _aiActionSubmitter;
        private readonly ICharacterQueue _characterQueue;

        private readonly GameStateMachine _gameStateMachine = new();

        public BattleService()
        {
            CharactersContainer = new CharactersContainer();
            _characterQueue = new CharacterQueue.CharacterQueue();
            _actionProcessor = new ActionProcessor();
            ActionSubmitter = new ActionSubmitter();
            _aiActionSubmitter = new AiActionSubmitter(this, ActionSubmitter, _characterQueue, CharactersContainer);
        }

        public IActionSubmitter ActionSubmitter { get; }
        public CharactersContainer CharactersContainer { get; }

        public bool IsBattleStarted { get; private set; }
        public bool IsBattleFinished { get; private set; }
        public UnityEvent<ETurnStep> OnTurnStepEnter { get; } = new();
        public UnityEvent OnTurnEnd { get; } = new();
        public UnityEvent<ActionInfo, ActionResultContainer> OnActionProcessingFinished { get; } = new();

        public void Init(List<CharacterInfo> characterInfos)
        {
            CharactersContainer.Init(characterInfos);
            _characterQueue.Init(CharactersContainer.Characters.Select(character => character.Value.CharacterData.Id));

            _gameStateMachine.Init(new List<ITurnStep>
            {
                new SelectTeamTurnStep(_characterQueue),
                new AwaitingInputTurnStep(ActionSubmitter),
                new ProcessActionsTurnStep(_actionProcessor)
            });

            _actionProcessor.Init(new List<IActionLogic>
            {
                new DamageActionLogic(CharactersContainer)
            });

            _aiActionSubmitter.Init();
        }

        public void Terminate()
        {
            _actionProcessor.OnActionProcessingFinished.RemoveListener(OnActionProcessingFinished.Invoke);
            _gameStateMachine.OnTurnEnd.RemoveListener(HandleTurnEnded);
            _gameStateMachine.OnStateEnter.RemoveListener(OnTurnStepEnter.Invoke);

            _aiActionSubmitter.Terminate();
            _characterQueue.Terminate();
            _actionProcessor.Terminate();
            CharactersContainer.Terminate();
            _gameStateMachine.Terminate();

            IsBattleStarted = false;
            IsBattleFinished = false;
        }

        public void StartBattle()
        {
            _gameStateMachine.OnTurnEnd.AddListener(HandleTurnEnded);
            _gameStateMachine.OnStateEnter.AddListener(OnTurnStepEnter.Invoke);
            _actionProcessor.OnActionProcessingFinished.AddListener(OnActionProcessingFinished.Invoke);

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

            OnTurnEnd.Invoke();
        }
    }
}