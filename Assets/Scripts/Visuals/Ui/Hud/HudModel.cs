namespace Visuals.Ui.Hud
{
    public class HudModel : IModel
    {
        public HudModel(IPlayerActionsHudModel playerActionsHudModel)
        {
            PlayerActionsHudModel = playerActionsHudModel;
        }

        public IPlayerActionsHudModel PlayerActionsHudModel { get; }
    }
}