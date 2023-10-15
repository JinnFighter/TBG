using Logic;
using Logic.Actions;
using UnityEngine.Events;

namespace Visuals
{
    public interface IVisualizerService
    {
        UnityEvent OnVisualizeStarted { get; }
        UnityEvent OnVisualizeFinished { get; }

        void Init(GameStateMachine gameStateMachine);
        void Terminate();
        void VisualizeAction(ActionInfo actionInfo, ActionResultContainer actionResultContainer);
    }
}