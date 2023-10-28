using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Visuals.Ui.Hud
{
    public class PlayerActionHudView : UiView
    {
        [field: SerializeField] public TextMeshProUGUI TextActionText { get; private set; }
        [field: SerializeField] public Button ButtonActionTrigger { get; private set; }
    }
}