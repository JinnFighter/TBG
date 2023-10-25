using System.Collections.Generic;
using Logic;
using Logic.BattleService;
using Logic.Characters;
using UnityEngine;
using Visuals.BattleArena;
using Visuals.Characters;
using Visuals.Ui.Hud;
using Visuals.UiService;
using Visuals.VisualizerLogic;

namespace Visuals
{
    public class BattleEntryPoint
    {
        private readonly BattleArenaSceneData _battleArenaSceneData;
        private readonly IBattleService _battleService;
        private readonly CharacterViewContainer _characterViewContainer;

        private readonly List<IController> _controllers = new();
        private readonly IUiService _uiService;
        private readonly IVisualizerService _visualizerService;
        private BattleCharactersModel _battleCharactersModel;
        private HudModel _hudModel;

        public BattleEntryPoint(BattleArenaSceneData battleArenaSceneData, IBattleService battleService,
            IVisualizerService visualizerService, CharacterViewContainer characterViewContainer, IUiService uiService)
        {
            _battleArenaSceneData = battleArenaSceneData;
            _battleService = battleService;
            _characterViewContainer = characterViewContainer;
            _uiService = uiService;
            _visualizerService = visualizerService;
        }

        public void Init()
        {
            _battleCharactersModel = new BattleCharactersModel(_battleService.CharactersContainer.Characters,
                _battleService.ActionSubmitter);

            foreach (var character in _battleCharactersModel.CharacterModels)
            {
                CharacterView characterView = character.Value.CharacterDataModel.TeamId.Value == ECharacterTeam.Player
                    ? _characterViewContainer.GetView<PlayerCharacterView>()
                    : _characterViewContainer.GetView<EnemyCharacterView>();

                var controller = new BattleCharacterController(_battleCharactersModel.CharacterModels[character.Key],
                    Object.Instantiate(characterView,
                        character.Value.CharacterDataModel.TeamId.Value == ECharacterTeam.Player
                            ? _battleArenaSceneData.CharacterSpawnSlotsView.PlayerTeamSpawnSlots[0].transform
                            : _battleArenaSceneData.CharacterSpawnSlotsView.EnemyTeamSpawnSlots[0].transform));
                _controllers.Add(controller);
            }

            _hudModel = new HudModel(_battleCharactersModel.CharacterModels[0]);

            _controllers.Add(new HudController(_hudModel, _uiService.OpenScreen<HudModel, HudView>(_hudModel)));

            _visualizerService.Init(new List<IVisualizerLogic>
            {
                new AttackVisualizerLogic(_battleCharactersModel)
            });

            foreach (var controller in _controllers) controller.Init();
        }

        public void Terminate()
        {
            foreach (var controller in _controllers) controller.Terminate();
            _uiService.CloseScreen<HudModel, HudView>(_hudModel);

            _controllers.Clear();

            _visualizerService.Terminate();
        }
    }
}