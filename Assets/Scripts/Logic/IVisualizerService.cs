using Logic.Actions;
using UnityEngine.Events;

namespace Logic
{
    public interface IVisualizerService
    {
        UnityEvent OnVisualizeStarted { get; }
        UnityEvent OnVisualizeFinished { get; }

        void Init();
        void Terminate();
        void VisualizeAction(ActionInfo actionInfo, ActionResultContainer actionResultContainer);
    }
}