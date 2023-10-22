using Logic.Actions;
using Reactivity;

namespace Visuals.Ui.Hud
{
    public class PlayerActionHudModel : IPlayerActionHudModel
    {
        private readonly IActionSubmitter _actionSubmitter;
        private readonly int _id;
        private readonly int _targetId;

        public PlayerActionHudModel(string actionName, int id, int targetId, IActionSubmitter actionSubmitter)
        {
            ActionName = new ReactiveProperty<string>(actionName);
            _actionSubmitter = actionSubmitter;
            _id = id;
            _targetId = targetId;
        }

        public IReactiveProperty<string> ActionName { get; }

        public void SubmitAction()
        {
            _actionSubmitter.SubmitAction(new ActionInfo
            {
                ActionId = ActionName.Value,
                CasterId = _id,
                TargetId = _targetId
            });
        }
    }
}