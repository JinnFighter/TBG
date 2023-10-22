namespace Visuals.Ui.Hud
{
    public class HudModel : IModel
    {
        public HudModel(IPlayerActionsHudModel playerActionsHudModel, ICharacterStatsModel characterStatsModel)
        {
            PlayerActionsHudModel = playerActionsHudModel;
            CharacterStatsModel = characterStatsModel;
        }

        public IPlayerActionsHudModel PlayerActionsHudModel { get; }
        public ICharacterStatsModel CharacterStatsModel { get; }
    }
}