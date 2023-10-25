using System.Collections.Generic;
using Logic.Actions;
using UnityEngine.Events;

namespace Logic
{
    public interface IVisualizerService
    {
        UnityEvent OnVisualizeStarted { get; }
        UnityEvent OnVisualizeFinished { get; }
        bool IsVisualizing { get; }

        void Init(List<IVisualizerLogic> logics);
        void Terminate();
        void VisualizeAction(ActionInfo actionInfo, ActionResultContainer actionResultContainer);
    }
}