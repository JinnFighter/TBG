using System.Collections.Generic;
using Logic;
using Logic.Actions;
using UnityEngine;
using UnityEngine.Events;

namespace Visuals
{
    public class VisualizerService : IVisualizerService
    {
        private List<IVisualizerLogic> _visualizerLogics;
        public UnityEvent OnVisualizeStarted { get; } = new();
        public UnityEvent OnVisualizeFinished { get; } = new();
        public bool IsVisualizing { get; private set; }

        public void Init(List<IVisualizerLogic> logics)
        {
            _visualizerLogics = logics;
        }

        public void Terminate()
        {
            _visualizerLogics.Clear();
        }

        public void VisualizeAction(ActionInfo actionInfo, ActionResultContainer actionResultContainer)
        {
            IsVisualizing = true;
            OnVisualizeStarted.Invoke();
            Debug.Log($"Action {actionInfo.ActionId} was casted by entity {actionInfo.CasterId}");
            foreach (var visualizerLogic in _visualizerLogics)
                visualizerLogic.VisualizeAction(actionInfo, actionResultContainer);

            IsVisualizing = false;
            OnVisualizeFinished.Invoke();
        }
    }
}