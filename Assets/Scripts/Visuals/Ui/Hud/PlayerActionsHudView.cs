using System.Collections.Generic;
using UnityEngine;

namespace Visuals.Ui.Hud
{
    public class PlayerActionsHudView : UiView
    {
        [field: SerializeField] public List<PlayerActionHudView> PlayerActions { get; private set; }
    }
}