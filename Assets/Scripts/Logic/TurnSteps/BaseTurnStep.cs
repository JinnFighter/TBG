using UnityEngine.Events;

namespace Logic.TurnSteps
{
    public abstract class BaseTurnStep : ITurnStep
    {
        public UnityEvent StepCompleted { get; } = new();
        public abstract ETurnStep Id { get; }

        public void EnterStep(TurnContext turnContext)
        {
            EnterStepInner(turnContext);
        }

        public void DoStep(TurnContext turnContext)
        {
            DoStepInner(turnContext);
        }

        public void ExitStep(TurnContext turnContext)
        {
            ExitStepInner(turnContext);
        }

        protected void MarkAsComplete()
        {
            StepCompleted.Invoke();
        }

        protected virtual void EnterStepInner(TurnContext turnContext)
        {
        }

        protected virtual void DoStepInner(TurnContext turnContext)
        {
        }

        protected virtual void ExitStepInner(TurnContext turnContext)
        {
        }
    }
}