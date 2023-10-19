namespace Visuals.Ui.Hud
{
    public class HudController : BaseController<HudModel, HudView>, IUiController
    {
        public HudController(HudModel model, HudView view) : base(model, view)
        {
        }
    }
}