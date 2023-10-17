using System.Collections.Generic;

namespace Logic.Actions
{
    public class ActionProcessor : IActionProcessor
    {
        private readonly List<IActionLogic> _actionLogics = new();

        public void Init()
        {
            _actionLogics.Add(new TestActionLogic());
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