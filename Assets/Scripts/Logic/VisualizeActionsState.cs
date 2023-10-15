using UnityEngine;
using UnityEngine.Events;

namespace Logic
{
    public class VisualizeActionsState : IGameState
    {
        private UnityAction _action;
        public EGameState Id => EGameState.VisualizeActions;

        public void EnterState(GameStateMachine gameStateMachine)
        {
            Debug.Log($"Enter state: {Id}");

            _action = () => HandleVisualizationFinished(gameStateMachine);
            gameStateMachine.OnVisualizationFinished.AddListener(_action);
            
            gameStateMachine.VisualizeAction();
        }

        private void HandleVisualizationFinished(GameStateMachine stateMachine)
        {
            stateMachine.OnVisualizationFinished.RemoveListener(_action);
            stateMachine.SetGameState<SelectTeamGameState>();
        }
    }
}