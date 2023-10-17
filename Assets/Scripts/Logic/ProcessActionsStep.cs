using Logic.Actions;
using Logic.TurnSteps;

namespace Logic
{
    public class ProcessActionsStep : BaseGameStep
    {
        private readonly IActionProcessor _actionProcessor;

        public ProcessActionsStep(IActionProcessor actionProcessor)
        {
            _actionProcessor = actionProcessor;
        }

        public override EGameStep Id => EGameStep.ProcessActions;

        protected override void DoStepInner(TurnContext turnContext)
        {
            if (turnContext.ActionInfo == null) return;
            var actionResult = _actionProcessor.ProcessAction(turnContext.ActionInfo);
            turnContext.ActionResult = actionResult;
            MarkAsComplete();
        }
    }
}