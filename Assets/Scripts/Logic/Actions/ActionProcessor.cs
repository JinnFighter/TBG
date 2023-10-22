using System.Collections.Generic;
using Logic.Actions.ActionLogic;

namespace Logic.Actions
{
    public class ActionProcessor : IActionProcessor
    {
        private List<IActionLogic> _actionLogics = new();

        public void Init(List<IActionLogic> actionLogics)
        {
            _actionLogics = actionLogics;
        }

        public void Terminate()
        {
            _actionLogics.Clear();
        }

        public ActionResultContainer ProcessAction(ActionInfo actionInfo)
        {
            var resultContainer = new ActionResultContainer();
            foreach (var actionLogic in _actionLogics) actionLogic.DoAction(actionInfo, resultContainer);
            return resultContainer;
        }
    }
}