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
using Visuals.VisualizerLogic;
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
        private BattleCharactersModel _battleCharactersModel;
        private List<BattleCharacterController> _characterControllers;
        private ICharacterQueue _characterQueue;
        private CharactersContainer _charactersContainer;
        private GameStateMachine _gameStateMachine;
        private HudController _hudController;

        private HudModel _hudModel;

        private IVisualizerService _visualizerService;

        private void Awake()
        {
            _charactersContainer = new CharactersContainer();
            _actionProcessor = new ActionProcessor(_charactersContainer);
            _actionSubmitter = new ActionSubmitter();
            _characterQueue = new CharacterQueue();

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

            _characterQueue.Init(_charactersContainer.Characters.Select(character => character.Value.Id));

            _actionProcessor.Init();
            _aiActionSubmitter.Init();

            _uiService.Init();

            _battleCharactersModel = new BattleCharactersModel(_charactersContainer.Characters);

            _visualizerService.Init(new List<IVisualizerLogic>
            {
                new AttackVisualizerLogic(_battleCharactersModel)
            });
            _hudModel = new HudModel(new PlayerActionsHudModel(new List<IPlayerActionHudModel>
            {
                new PlayerActionHudModel("attack", 0, 1, _actionSubmitter)
            }), _battleCharactersModel.CharacterModels[0].CharacterStatsModel);

            _hudController =
                new HudController(_hudModel, _uiService.OpenScreen<HudModel, HudView>(_hudModel));
            _hudController.Init();

            _characterViewContainer.Init();

            _characterControllers = new List<BattleCharacterController>();
            foreach (var character in _battleCharactersModel.CharacterModels)
            {
                CharacterView characterView = character.Value.Team.Value == ECharacterTeam.Player
                    ? _characterViewContainer.GetView<PlayerCharacterView>()
                    : _characterViewContainer.GetView<EnemyCharacterView>();

                var controller = new BattleCharacterController(_battleCharactersModel.CharacterModels[character.Key],
                    Instantiate(characterView,
                        character.Value.Team.Value == ECharacterTeam.Player
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