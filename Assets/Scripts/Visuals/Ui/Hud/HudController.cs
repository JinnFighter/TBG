namespace Visuals.Ui.Hud
{
    public class HudController : BaseController<HudModel, HudView>, IUiController
    {
        public HudController(HudModel model, HudView view) : base(model, view)
        {
        }

        protected override void InitInner()
        {
            RegisterChildController(new CharacterStatsHudController(Model.CharacterStatsModel, View.StatsHudView));
            RegisterChildController(new PlayerActionsHudController(Model.PlayerActionsHudModel,
                View.PlayerActionsHudView));
        }
    }
}