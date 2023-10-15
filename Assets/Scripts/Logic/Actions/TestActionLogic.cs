namespace Logic.Actions
{
    public class TestActionLogic : IActionLogic
    {
        public void DoAction(ActionInfo actionInfo, ActionResultContainer resultContainer)
        {
            resultContainer.RegisterResult(new TestActionResult());
        }
    }
}