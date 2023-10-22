using Reactivity;

namespace Visuals.Ui.Hud
{
    public class CharacterStatsHudController : BaseController<ICharacterStatsModel, CharacterStatsHudView>
    {
        public CharacterStatsHudController(ICharacterStatsModel model, CharacterStatsHudView view) : base(model, view)
        {
        }

        protected override void InitInner()
        {
            SubscriptionAggregator.ListenEvent(Model.CharacterName, HandleCharacterNameChanged, true);
            SubscriptionAggregator.ListenEvent(Model.CurrentHealth, HandleCurrentHealthChanged, true);
            SubscriptionAggregator.ListenEvent(Model.MaxHealth, HandleMaxHealthChanged, true);
        }

        private void HandleCharacterNameChanged(object sender, GenericEventArg<string> e)
        {
            View.TextCharacterName.text = $"{e.Value}";
        }

        private void HandleCurrentHealthChanged(object sender, GenericEventArg<int> e)
        {
            View.TextCurrentHealth.text = $"{e.Value}";
        }

        private void HandleMaxHealthChanged(object sender, GenericEventArg<int> e)
        {
            View.TextMaxHealth.text = $"{e.Value}";
        }
    }
}