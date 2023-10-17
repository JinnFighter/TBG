using System.Collections.Generic;
using Logic;
using Logic.Actions;
using Logic.Characters;
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
            _gameStateMachine.Init(new List<IGameStep>
            {
                new SelectTeamGameStep(_charactersContainer),
                new AwaitingInputGameStep(_actionSubmitter),
                new ProcessActionsStep(_actionProcessor),
                new VisualizeActionsStep(_visualizerService)
            });

            _charactersContainer.Init(new List<CharacterInfo>
            {
                new(0, "Player", CharactersContainer.PlayerTeamId),
                new(1, "Enemy", CharactersContainer.EnemyTeamId)
            });

            _actionProcessor.Init();

            _visualizerService.Init();
            _gameStateMachine.OnStateEnter.AddListener(HandleStepEnter);
            _gameStateMachine.GoToNextState();
        }

        private void Update()
        {
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

        private void HandleStepEnter(EGameStep step)
        {
            if (step != EGameStep.AwaitingInput) return;

            if (_charactersContainer.CurrentTeamId == CharactersContainer.EnemyTeamId)
                _actionSubmitter.SubmitAction(new ActionInfo
                {
                    ActionId = "test",
                    CasterId = 1
                });
        }
    }
}