namespace Visuals.Ui.Hud
{
    public class PlayerActionsHudController : BaseController<IPlayerActionsHudModel, PlayerActionsHudView>
    {
        public PlayerActionsHudController(IPlayerActionsHudModel model, PlayerActionsHudView view) : base(model, view)
        {
        }

        protected override void InitInner()
        {
            for (var index = 0; index < Model.PlayerActions.Count; index++)
            {
                var playerActionHudModel = Model.PlayerActions[index];
                RegisterChildController(new PlayerActionHudController(playerActionHudModel, View.PlayerActions[index]));
            }
        }
    }
}