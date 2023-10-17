using Logic.Actions;
using Logic.TurnSteps;
using UnityEngine.Events;

namespace Logic
{
    public class AwaitingInputGameStep : BaseGameStep
    {
        private readonly IActionSubmitter _actionSubmitter;
        private UnityAction<ActionInfo> _action;

        public AwaitingInputGameStep(IActionSubmitter actionSubmitter)
        {
            _actionSubmitter = actionSubmitter;
        }

        public override EGameStep Id => EGameStep.AwaitingInput;

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