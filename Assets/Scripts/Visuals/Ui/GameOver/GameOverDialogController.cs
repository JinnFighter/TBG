using UnityEngine;
using UnityEngine.SceneManagement;

namespace Visuals.Ui.GameOver
{
    public class GameOverDialogController : BaseController<GameOverDialogModel, GameOverDialogView>
    {
        public GameOverDialogController(GameOverDialogModel model, GameOverDialogView view) : base(model, view)
        {
        }

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