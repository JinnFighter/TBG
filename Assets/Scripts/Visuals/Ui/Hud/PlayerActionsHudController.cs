using Visuals.UiService;

namespace Visuals.Ui.Hud
{
    public class PlayerActionsHudController : UiEmbeddedWidget<IPlayerAbilitiesHudModel, PlayerActionsHudView>
    {
        protected override void InitInner()
        {
            for (var index = 0; index < Model.PlayerActions.Count; index++)
            {
                var playerActionHudModel = Model.PlayerActions[index];
                RegisterChildWidget<PlayerActionHudController>(playerActionHudModel, View.PlayerActions[index]);
            }
        }
    }
}