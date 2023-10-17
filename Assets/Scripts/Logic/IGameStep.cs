using Logic.TurnSteps;
using UnityEngine.Events;

namespace Logic
{
    public interface IGameStep
    {
        UnityEvent StepCompleted { get; }
        EGameStep Id { get; }
        void EnterStep(TurnContext turnContext);
        void DoStep(TurnContext turnContext);
        void ExitStep(TurnContext turnContext);
    }
}