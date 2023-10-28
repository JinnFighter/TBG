using Visuals.UiService;

namespace Visuals.Ui.Hud
{
    public class HudWidget : UiScreen<HudModel, HudView>
    {
        protected override void InitInner()
        {
            RegisterChildWidget<CharacterStatsHudController>(Model.CharacterStatsModel, View.StatsHudView);
            RegisterChildWidget<PlayerActionsHudController>(Model.PlayerAbilitiesHudModel, View.PlayerActionsHudView);
        }
    }
}