using UnityEngine;
using UnityEngine.UI;

namespace Visuals.Ui.GameOver
{
    public class GameOverDialogView : UiView
    {
        [field: SerializeField] public Button ButtonRestart { get; private set; }
        [field: SerializeField] public Button ButtonQuit { get; private set; }
    }
}