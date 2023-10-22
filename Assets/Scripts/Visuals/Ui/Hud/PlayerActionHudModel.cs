using Logic.Actions;
using Reactivity;

namespace Visuals.Ui.Hud
{
    public class PlayerActionHudModel : IPlayerActionHudModel
    {
        private readonly IActionSubmitter _actionSubmitter;
        private readonly int _id;

        public PlayerActionHudModel(string actionName, int id, IActionSubmitter actionSubmitter)
        {
            ActionName = new ReactiveProperty<string>(actionName);
            _actionSubmitter = actionSubmitter;
            _id = id;
        }

        public IReactiveProperty<string> ActionName { get; }

        public void SubmitAction()
        {
            _actionSubmitter.SubmitAction(new ActionInfo
            {
                ActionId = ActionName.Value,
                CasterId = _id
            });
        }
    }
}