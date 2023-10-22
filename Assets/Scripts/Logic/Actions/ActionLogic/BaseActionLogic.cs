namespace Logic.Actions.ActionLogic
{
    public abstract class BaseActionLogic : IActionLogic
    {
        protected abstract string ActionId { get; }

        public void DoAction(ActionInfo actionInfo, ActionResultContainer resultContainer)
        {
            if (ActionId == actionInfo.ActionId && CanDoAction(actionInfo)) DoActionInner(actionInfo, resultContainer);
        }

        protected virtual bool CanDoAction(ActionInfo actionInfo)
        {
            return true;
        }

        protected abstract void DoActionInner(ActionInfo actionInfo, ActionResultContainer actionResultContainer);
    }
}