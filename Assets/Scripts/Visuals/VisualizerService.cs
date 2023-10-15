using Logic;
using Logic.Actions;
using UnityEngine;
using UnityEngine.Events;

namespace Visuals
{
    public class VisualizerService : IVisualizerService
    {
        private GameStateMachine _gameStateMachine;
        public UnityEvent OnVisualizeStarted { get; } = new();
        public UnityEvent OnVisualizeFinished { get; } = new();

        public void Init(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _gameStateMachine.OnVisualizationStarted.AddListener(VisualizeAction);
        }

        public void Terminate()
        {
            _gameStateMachine.OnVisualizationStarted.RemoveListener(VisualizeAction);
            _gameStateMachine = null;
        }

        public void VisualizeAction(ActionInfo actionInfo, ActionResultContainer actionResultContainer)
        {
            OnVisualizeStarted.Invoke();
            Debug.Log($"Action {actionInfo.ActionId} was casted by entity {actionInfo.CasterId}");
            _gameStateMachine.OnVisualizationFinished.Invoke();
            OnVisualizeFinished.Invoke();
        }
    }
}