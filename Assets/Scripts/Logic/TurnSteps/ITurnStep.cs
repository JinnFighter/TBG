using UnityEngine.Events;

namespace Logic.TurnSteps
{
    public interface ITurnStep
    {
        UnityEvent StepCompleted { get; }
        ETurnStep Id { get; }
        void EnterStep(TurnContext turnContext);
        void DoStep(TurnContext turnContext);
        void ExitStep(TurnContext turnContext);
    }
}