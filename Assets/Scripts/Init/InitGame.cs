using System.Collections.Generic;
using System.Linq;
using Logic;
using Logic.Actions;
using Logic.AI;
using Logic.CharacterQueue;
using Logic.Characters;
using Logic.TurnSteps;
using UnityEngine;
using Visuals;
using Visuals.BattleField;
using Visuals.Characters;
using Visuals.Ui.Hud;
using Visuals.UiService;
using CharacterInfo = Logic.Characters.CharacterInfo;

namespace Init
{
    public class InitGame : MonoBehaviour
    {
        [SerializeField] private UiService _uiService;

        [SerializeField] private CharacterViewContainer _characterViewContainer;
        [SerializeField] private CharacterSpawnSlotsView _characterSpawnSlotsView;
        private IActionProcessor _actionProcessor;
        private IActionSubmitter _actionSubmitter;
        private IAiActionSubmitter _aiActionSubmitter;
        private List<BattleCharacterController> _characterControllers;
        private ICharacterQueue _characterQueue;
        private CharactersContainer _charactersContainer;
        private GameStateMachine _gameStateMachine;
        private HudController _hudController;

        private HudModel _hudModel;

        private IVisualizerService _visualizerService;

        private void Awake()
        {
            _actionProcessor = new ActionProcessor();
            _actionSubmitter = new ActionSubmitter();
            _characterQueue = new CharacterQueue();
            _charactersContainer = new CharactersContainer();
            _gameStateMachine = new GameStateMachine();
            _aiActionSubmitter = new AiActionSubmitter(_gameStateMachine, _actionSubmitter, _characterQueue);

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
                new(0, "Player", ECharacterTeam.Player, new CharacterStats(10, 10)),
                new(1, "Enemy", ECharacterTeam.Enemy, new CharacterStats(10, 10))
            });

            _actionProcessor.Init();

            _characterQueue.Init(_charactersContainer.Characters.Select(character => character.Value.Id));
            _aiActionSubmitter.Init();

            _visualizerService.Init();
            _uiService.Init();

            _hudModel = new HudModel(new PlayerActionsHudModel(new List<IPlayerActionHudModel>
            {
                new PlayerActionHudModel("test", 0, _actionSubmitter)
            }));

            _hudController =
                new HudController(_hudModel, _uiService.OpenScreen<HudModel, HudView>(_hudModel));
            _hudController.Init();

            _characterViewContainer.Init();

            _characterControllers = new List<BattleCharacterController>();
            foreach (var character in _charactersContainer.Characters)
            {
                CharacterView characterView = character.Value.TeamId == ECharacterTeam.Player
                    ? _characterViewContainer.GetView<PlayerCharacterView>()
                    : _characterViewContainer.GetView<EnemyCharacterView>();

                var controller = new BattleCharacterController(new CharacterModel(),
                    Instantiate(characterView,
                        character.Value.TeamId == ECharacterTeam.Player
                            ? _characterSpawnSlotsView.PlayerTeamSpawnSlots[0].transform
                            : _characterSpawnSlotsView.EnemyTeamSpawnSlots[0].transform));
                _characterControllers.Add(controller);
                controller.Init();
            }

            _gameStateMachine.GoToNextState();
        }

        private void OnDestroy()
        {
            foreach (var characterController in _characterControllers) characterController.Terminate();
            _hudController.Terminate();
            _uiService.Terminate();
            _aiActionSubmitter.Terminate();
            _characterQueue.Terminate();
            _actionProcessor.Terminate();
            _charactersContainer.Terminate();
            _gameStateMachine.Terminate();
        }
    }
}