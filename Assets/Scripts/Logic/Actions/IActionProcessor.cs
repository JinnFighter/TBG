namespace Logic.Actions
{
    public interface IActionProcessor
    {
        void Init();
        void Terminate();
        ActionResultContainer ProcessAction(ActionInfo actionInfo);
    }
}