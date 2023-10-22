using System.Collections.Generic;
using UnityEngine;

namespace Visuals.Ui.Hud
{
    public class PlayerActionsHudView : BaseView
    {
        [field: SerializeField] public List<PlayerActionHudView> PlayerActions { get; private set; }
    }
}