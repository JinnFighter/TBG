using Logic.Actions;

namespace Logic.TurnSteps
{
    public class ProcessActionsTurnStep : BaseTurnStep
    {
        private readonly IActionProcessor _actionProcessor;

        public ProcessActionsTurnStep(IActionProcessor actionProcessor)
        {
            _actionProcessor = actionProcessor;
        }

        public override ETurnStep Id => ETurnStep.ProcessActions;

        protected override void DoStepInner(TurnContext turnContext)
        {
            if (turnContext.ActionInfo == null) return;
            var actionResult = _actionProcessor.ProcessAction(turnContext.ActionInfo);
            turnContext.ActionResult = actionResult;
            MarkAsComplete();
        }
    }
}