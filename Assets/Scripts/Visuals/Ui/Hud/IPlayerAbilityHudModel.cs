using Reactivity;

namespace Visuals.Ui.Hud
{
    public interface IPlayerAbilityHudModel : IModel
    {
        IReactiveProperty<string> ActionName { get; }
        void SubmitAction();
    }
}