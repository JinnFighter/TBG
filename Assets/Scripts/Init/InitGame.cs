using System.Collections.Generic;
using Logic;
using Logic.Actions;
using Logic.Characters;
using Logic.TurnSteps;
using UnityEngine;
using Visuals;
using CharacterInfo = Logic.Characters.CharacterInfo;

namespace Init
{
    public class InitGame : MonoBehaviour
    {
        private IActionProcessor _actionProcessor;
        private IActionSubmitter _actionSubmitter;
        private CharactersContainer _charactersContainer;
        private GameStateMachine _gameStateMachine;

        private ETurnStep _turnStep = ETurnStep.Invalid;
        private IVisualizerService _visualizerService;

        private void Awake()
        {
            _actionProcessor = new ActionProcessor();
            _actionSubmitter = new ActionSubmitter();
            _charactersContainer = new CharactersContainer();
            _gameStateMachine = new GameStateMachine();
            _visualizerService = new VisualizerService();
        }

        private void Start()
        {
            _gameStateMachine.Init(new List<ITurnStep>
            {
                new SelectTeamTurnStep(_charactersContainer),
                new AwaitingInputTurnStep(_actionSubmitter),
                new ProcessActionsTurnStep(_actionProcessor),
                new VisualizeActionsTurnStep(_visualizerService),
                new CheckTurnOverTurnStep(_charactersContainer)
            });

            _charactersContainer.Init(new List<CharacterInfo>
            {
                new(0, "Player", CharactersContainer.PlayerTeamId, new CharacterStats(10, 10)),
                new(1, "Enemy", CharactersContainer.EnemyTeamId, new CharacterStats(10, 10))
            });

            _actionProcessor.Init();

            _visualizerService.Init();
            _gameStateMachine.OnStateEnter.AddListener(HandleStepEnter);
            _gameStateMachine.GoToNextState();
        }

        private void Update()
        {
            if (_turnStep != ETurnStep.AwaitingInput) return;

            if (Input.GetMouseButtonDown(0) && _charactersContainer.CurrentTeamId == CharactersContainer.PlayerTeamId)
                _actionSubmitter.SubmitAction(new ActionInfo
                {
                    ActionId = "test",
                    CasterId = 0
                });
        }

        private void OnDestroy()
        {
            _gameStateMachine.OnStateEnter.RemoveListener(HandleStepEnter);
            _actionProcessor.Terminate();
            _charactersContainer.Terminate();
            _gameStateMachine.Terminate();
        }

        private void HandleStepEnter(ETurnStep step)
        {
            _turnStep = step;
            if (step != ETurnStep.AwaitingInput) return;

            if (_charactersContainer.CurrentTeamId == CharactersContainer.EnemyTeamId)
                _actionSubmitter.SubmitAction(new ActionInfo
                {
                    ActionId = "test",
                    CasterId = 1
                });
        }
    }
}