using System.Collections.Generic;
using System.Linq;
using Logic;
using Logic.Actions;
using Logic.CharacterQueue;
using Logic.Characters;
using Logic.TurnSteps;
using UnityEngine;
using Visuals;
using Visuals.Ui.Hud;
using Visuals.UiService;
using CharacterInfo = Logic.Characters.CharacterInfo;

namespace Init
{
    public class InitGame : MonoBehaviour
    {
        private IActionProcessor _actionProcessor;
        private IActionSubmitter _actionSubmitter;
        private ICharacterQueue _characterQueue;
        private CharactersContainer _charactersContainer;
        private GameStateMachine _gameStateMachine;

        private ETurnStep _turnStep = ETurnStep.Invalid;
        private IVisualizerService _visualizerService;

        [SerializeField] private UiService _uiService;

        private HudModel _hudModel;
        private HudController _hudController;

        private void Awake()
        {
            _actionProcessor = new ActionProcessor();
            _actionSubmitter = new ActionSubmitter();
            _characterQueue = new CharacterQueue();
            _charactersContainer = new CharactersContainer();
            _gameStateMachine = new GameStateMachine();
            _visualizerService = new VisualizerService();
        }

        private void Start()
        {
            _gameStateMachine.Init(new List<ITurnStep>
            {
                new SelectTeamTurnStep(_characterQueue),
                new AwaitingInputTurnStep(_actionSubmitter),
                new ProcessActionsTurnStep(_actionProcessor),
                new VisualizeActionsTurnStep(_visualizerService),
                new CheckTurnOverTurnStep(_charactersContainer)
            });

            _charactersContainer.Init(new List<CharacterInfo>
            {
                new(0, "Player", CharacterConst.PlayerTeamId, new CharacterStats(10, 10)),
                new(1, "Enemy", CharacterConst.EnemyTeamId, new CharacterStats(10, 10))
            });

            _actionProcessor.Init();
            
            _characterQueue.Init(_charactersContainer.Characters.Select(character => character.Value.Id));

            _visualizerService.Init();
            _uiService.Init();

            _hudModel = new HudModel();
            _hudController =
                new HudController(_hudModel, _uiService.OpenScreen<HudModel, HudView>(_hudModel, "hudView"));
            _hudController.Init();
            
            _gameStateMachine.OnStateEnter.AddListener(HandleStepEnter);
            _gameStateMachine.GoToNextState();
        }

        private void Update()
        {
            if (_turnStep != ETurnStep.AwaitingInput) return;

            if (Input.GetMouseButtonDown(0) && _characterQueue.CurrentTeamId == CharacterConst.PlayerTeamId)
                _actionSubmitter.SubmitAction(new ActionInfo
                {
                    ActionId = "test",
                    CasterId = 0
                });
        }

        private void OnDestroy()
        {
            _hudController.Terminate();
            _uiService.Terminate();
            _gameStateMachine.OnStateEnter.RemoveListener(HandleStepEnter);
            _characterQueue.Terminate();
            _actionProcessor.Terminate();
            _charactersContainer.Terminate();
            _gameStateMachine.Terminate();
        }

        private void HandleStepEnter(ETurnStep step)
        {
            _turnStep = step;
            if (step != ETurnStep.AwaitingInput) return;

            if (_characterQueue.CurrentTeamId == CharacterConst.EnemyTeamId)
                _actionSubmitter.SubmitAction(new ActionInfo
                {
                    ActionId = "test",
                    CasterId = 1
                });
        }
    }
}