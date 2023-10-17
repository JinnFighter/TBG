using Logic;
using Logic.Actions;
using UnityEngine;
using UnityEngine.Events;

namespace Visuals
{
    public class VisualizerService : IVisualizerService
    {
        public UnityEvent OnVisualizeStarted { get; } = new();
        public UnityEvent OnVisualizeFinished { get; } = new();

        public void Init()
        {
        }

        public void Terminate()
        {
        }

        public void VisualizeAction(ActionInfo actionInfo, ActionResultContainer actionResultContainer)
        {
            OnVisualizeStarted.Invoke();
            Debug.Log($"Action {actionInfo.ActionId} was casted by entity {actionInfo.CasterId}");
            OnVisualizeFinished.Invoke();
        }
    }
}