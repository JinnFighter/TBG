using UnityEngine;
using UnityEngine.SceneManagement;
using Visuals.UiService;

namespace Visuals.Ui.GameOver
{
    public class GameOverDialogController : UiDialog<GameOverDialogModel, GameOverDialogView>
    {
        protected override void InitInner()
        {
            SubscriptionAggregator.ListenEvent(View.ButtonRestart.onClick, HandleButtonRestartClicked);
            SubscriptionAggregator.ListenEvent(View.ButtonQuit.onClick, HandleQuitButtonClicked);
        }

        private void HandleQuitButtonClicked()
        {
            Application.Quit();
        }

        private void HandleButtonRestartClicked()
        {
            SceneManager.LoadScene("Scenes/Battle");
        }
    }
}