using UnityEngine;

namespace Visuals.Ui.Hud
{
    public class HudView : BaseView
    {
        [field: SerializeField] public CharacterStatsHudView StatsHudView { get; private set; }
        [field: SerializeField] public PlayerActionsHudView PlayerActionsHudView { get; private set; }
    }
}