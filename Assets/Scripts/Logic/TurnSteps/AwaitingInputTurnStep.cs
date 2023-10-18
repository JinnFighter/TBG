using Logic.Actions;
using UnityEngine.Events;

namespace Logic.TurnSteps
{
    public class AwaitingInputTurnStep : BaseTurnStep
    {
        private readonly IActionSubmitter _actionSubmitter;
        private UnityAction<ActionInfo> _action;

        public AwaitingInputTurnStep(IActionSubmitter actionSubmitter)
        {
            _actionSubmitter = actionSubmitter;
        }

        public override ETurnStep Id => ETurnStep.AwaitingInput;

        protected override void EnterStepInner(TurnContext turnContext)
        {
            _action = actionInfo => HandleActionSubmitted(actionInfo, turnContext);
            _actionSubmitter.OnActionSubmitted.AddListener(_action);
        }

        protected override void ExitStepInner(TurnContext context)
        {
            _actionSubmitter.OnActionSubmitted.RemoveListener(_action);
        }

        private void HandleActionSubmitted(ActionInfo actionInfo, TurnContext turnContext)
        {
            turnContext.ActionInfo = actionInfo;
            MarkAsComplete();
        }
    }
}