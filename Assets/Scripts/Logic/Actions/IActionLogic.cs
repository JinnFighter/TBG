namespace Logic.Actions
{
    public interface IActionLogic
    {
        void DoAction(ActionInfo actionInfo, ActionResultContainer resultContainer);
    }
}