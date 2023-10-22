using Reactivity;

namespace Visuals.Ui.Hud
{
    public class PlayerActionHudController : BaseController<IPlayerAbilityHudModel, PlayerActionHudView>
    {
        public PlayerActionHudController(IPlayerAbilityHudModel model, PlayerActionHudView view) : base(model, view)
        {
        }

        protected override void InitInner()
        {
            SubscriptionAggregator.ListenEvent(Model.ActionName, HandleActionNameChanged, true);
            SubscriptionAggregator.ListenEvent(View.ButtonActionTrigger.onClick, HandleButtonActionClicked);
        }

        private void HandleButtonActionClicked()
        {
            Model.SubmitAction();
        }

        private void HandleActionNameChanged(object sender, GenericEventArg<string> e)
        {
            View.TextActionText.text = e.Value;
        }
    }
}