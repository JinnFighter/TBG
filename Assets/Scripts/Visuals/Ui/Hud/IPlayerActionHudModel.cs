using Reactivity;

namespace Visuals.Ui.Hud
{
    public interface IPlayerActionHudModel : IModel
    {
        IReactiveProperty<string> ActionName { get; }
        void SubmitAction();
    }
}