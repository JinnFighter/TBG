using System.Collections.Generic;
using Logic;
using Logic.BattleService;
using Logic.Characters;
using Logic.Config;
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

        [SerializeField] private AttackAbilityConfig attackAbilityConfig;
        private BattleCharactersModel _battleCharactersModel;

        private IBattleService _battleService;
        private List<BattleCharacterController> _characterControllers;

        private HudController _hudController;

        private HudModel _hudModel;

        private IVisualizerService _visualizerService;

        private void Awake()
        {
            _battleService = new BattleService();
            _visualizerService = new VisualizerService(_battleService.ActionProcessor);
        }

        private void Start()
        {
            _battleService.Init();

            _uiService.Init();

            _battleService.StartBattle(new List<CharacterInfo>
            {
                new(new CharacterData(0, "Player", ECharacterTeam.Player), new CharacterStats(10, 10),
                    new CharacterAbilities(
                        new List<BaseAbilityConfig>
                        {
                            attackAbilityConfig
                        })),
                new(new CharacterData(1, "Enemy", ECharacterTeam.Enemy), new CharacterStats(10, 10),
                    new CharacterAbilities(
                        new List<BaseAbilityConfig>
                        {
                            attackAbilityConfig
                        }))
            });

            _battleCharactersModel = new BattleCharactersModel(_battleService.CharactersContainer.Characters,
                _battleService.ActionSubmitter);

            _visualizerService.Init(new List<IVisualizerLogic>
            {
                new AttackVisualizerLogic(_battleCharactersModel, attackAbilityConfig)
            });

            _hudModel = new HudModel(_battleCharactersModel.CharacterModels[0]);

            _hudController =
                new HudController(_hudModel, _uiService.OpenScreen<HudModel, HudView>(_hudModel));
            _hudController.Init();

            _characterViewContainer.Init();

            _characterControllers = new List<BattleCharacterController>();
            foreach (var character in _battleCharactersModel.CharacterModels)
            {
                CharacterView characterView = character.Value.CharacterDataModel.TeamId.Value == ECharacterTeam.Player
                    ? _characterViewContainer.GetView<PlayerCharacterView>()
                    : _characterViewContainer.GetView<EnemyCharacterView>();

                var controller = new BattleCharacterController(_battleCharactersModel.CharacterModels[character.Key],
                    Instantiate(characterView,
                        character.Value.CharacterDataModel.TeamId.Value == ECharacterTeam.Player
                            ? _characterSpawnSlotsView.PlayerTeamSpawnSlots[0].transform
                            : _characterSpawnSlotsView.EnemyTeamSpawnSlots[0].transform));
                _characterControllers.Add(controller);
                controller.Init();
            }
        }

        private void OnDestroy()
        {
            foreach (var characterController in _characterControllers) characterController.Terminate();
            _visualizerService.Terminate();
            _hudController.Terminate();
            _uiService.Terminate();

            _battleService.Terminate();
        }
    }
}