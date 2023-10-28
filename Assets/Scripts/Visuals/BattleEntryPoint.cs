using System.Collections.Generic;
using System.Linq;
using Logic.Actions;
using Logic.BattleService;
using Logic.Characters;
using UnityEngine;
using Visuals.BattleArena;
using Visuals.Characters;
using Visuals.Ui.GameOver;
using Visuals.Ui.Hud;
using Visuals.Ui.TargetSelection;
using Visuals.UiService;
using Visuals.VisualizerLogic;

namespace Visuals
{
    public class BattleEntryPoint
    {
        private readonly Queue<(ActionInfo, ActionResultContainer)> _actionResults = new();
        private readonly BattleArenaSceneData _battleArenaSceneData;
        private readonly IBattleService _battleService;
        private readonly CharacterViewContainer _characterViewContainer;

        private readonly List<IController> _controllers = new();
        private readonly IUiService _uiService;
        private readonly IVisualizerService _visualizerService;

        private BattleCharactersModel _battleCharactersModel;
        private CharacterSpawnSlotsModel _enemySpawnSlots;
        private ITargetSelectorModel _enemyTargetSelectorModel;
        private HudModel _hudModel;
        private CharacterSpawnSlotsModel _playerSpawnSlots;
        private ITargetSelectorModel _playerTargetSelectorModel;

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

            _playerSpawnSlots =
                new CharacterSpawnSlotsModel(_battleCharactersModel.TeamCharacterModels[ECharacterTeam.Player].Count);
            _enemySpawnSlots =
                new CharacterSpawnSlotsModel(_battleCharactersModel.TeamCharacterModels[ECharacterTeam.Enemy].Count);

            var characters = _battleCharactersModel.CharacterModels.ToList();
            for (var i = 0; i < characters.Count; i++)
            {
                var character = characters[i];
                CharacterView characterView = character.Value.CharacterDataModel.TeamId.Value == ECharacterTeam.Player
                    ? _characterViewContainer.GetView<PlayerCharacterView>()
                    : _characterViewContainer.GetView<EnemyCharacterView>();

                var slotId = character.Value.CharacterDataModel.TeamId.Value == ECharacterTeam.Player
                    ? _playerSpawnSlots.SpawnAtSlot(character.Value.CharacterDataModel.Id.Value)
                    : _enemySpawnSlots.SpawnAtSlot(character.Value.CharacterDataModel.Id.Value);
                var controller = new BattleCharacterController(_battleCharactersModel.CharacterModels[character.Key],
                    Object.Instantiate(characterView,
                        character.Value.CharacterDataModel.TeamId.Value == ECharacterTeam.Player
                            ? _battleArenaSceneData.CharacterSpawnSlotsView.PlayerTeamSpawnSlots[slotId].transform
                            : _battleArenaSceneData.CharacterSpawnSlotsView.EnemyTeamSpawnSlots[slotId].transform));
                _controllers.Add(controller);
            }

            _playerTargetSelectorModel = new TargetSelectorModel(_playerSpawnSlots, ECharacterTeam.Player);
            _enemyTargetSelectorModel = new TargetSelectorModel(_enemySpawnSlots, ECharacterTeam.Player);

            _hudModel = new HudModel(_battleCharactersModel.CharacterModels[0]);

            _uiService.Open<HudWidget>(_hudModel, typeof(HudView));

            _visualizerService.Init(new List<IVisualizerLogic>
            {
                new AttackVisualizerLogic(_battleCharactersModel)
            });

            foreach (var controller in _controllers) controller.Init();

            _battleService.OnActionProcessingFinished.AddListener(HandleProcessingFinished);
            _battleService.OnTurnEnd.AddListener(HandleTurnEnded);
        }

        private void HandleTurnEnded()
        {
            if (_visualizerService.IsVisualizing || _actionResults.Count == 0) return;
            VisualizeAction();
        }

        private async void VisualizeAction()
        {
            while (_actionResults.Count > 0)
            {
                var actionResult = _actionResults.Dequeue();
                await _visualizerService.VisualizeAction(actionResult.Item1, actionResult.Item2);
            }

            if (!_battleService.IsBattleFinished) return;

            _uiService.Open<GameOverDialogController>(new GameOverDialogModel(), typeof(GameOverDialogView));
        }

        private void HandleProcessingFinished(ActionInfo actionInfo, ActionResultContainer resultContainer)
        {
            _actionResults.Enqueue((actionInfo, resultContainer));
        }

        public void Terminate()
        {
            _battleService.OnTurnEnd.RemoveListener(HandleTurnEnded);
            _battleService.OnActionProcessingFinished.RemoveListener(HandleProcessingFinished);

            foreach (var controller in _controllers) controller.Terminate();
            _uiService.Close<HudWidget>(_hudModel);

            _controllers.Clear();

            _visualizerService.Terminate();
        }
    }
}