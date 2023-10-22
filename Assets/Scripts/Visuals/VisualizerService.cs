using System.Collections.Generic;
using Logic;
using Logic.Actions;
using UnityEngine;
using UnityEngine.Events;

namespace Visuals
{
    public class VisualizerService : IVisualizerService
    {
        private readonly IActionProcessor _actionProcessor;

        public VisualizerService(IActionProcessor actionProcessor)
        {
            _actionProcessor = actionProcessor;
        }

        private List<IVisualizerLogic> _visualizerLogics;
        public UnityEvent OnVisualizeStarted { get; } = new();
        public UnityEvent OnVisualizeFinished { get; } = new();

        public void Init(List<IVisualizerLogic> logics)
        {
            _visualizerLogics = logics;
            _actionProcessor.OnActionProcessingFinished.AddListener(HandleActionProcessingFinished);
        }

        public void Terminate()
        {
            _actionProcessor.OnActionProcessingFinished.RemoveListener(HandleActionProcessingFinished);
            _visualizerLogics.Clear();
        }

        public void VisualizeAction(ActionInfo actionInfo, ActionResultContainer actionResultContainer)
        {
            OnVisualizeStarted.Invoke();
            Debug.Log($"Action {actionInfo.ActionId} was casted by entity {actionInfo.CasterId}");
            foreach (var visualizerLogic in _visualizerLogics)
                visualizerLogic.VisualizeAction(actionInfo, actionResultContainer);

            OnVisualizeFinished.Invoke();
        }
        
        private void HandleActionProcessingFinished(ActionInfo actionInfo, ActionResultContainer resultContainer)
        {
            VisualizeAction(actionInfo, resultContainer);
        }
    }
}