namespace Logic.Actions.ActionLogic
{
    public interface IActionLogic
    {
        void DoAction(ActionInfo actionInfo, ActionResultContainer resultContainer);
    }
}