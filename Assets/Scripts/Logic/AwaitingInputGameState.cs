using Logic.Actions;
using UnityEngine;
using UnityEngine.Events;

namespace Logic
{
    public class AwaitingInputGameState : IGameState
    {
        public EGameState Id => EGameState.AwaitingInput;
        private UnityAction<ActionInfo> _action;

        public void EnterState(GameStateMachine gameStateMachine)
        {
            Debug.Log($"Enter state: {Id}");

            gameStateMachine.ResetSubmittedAction();
            _action = _ => HandleActionSubmitted(gameStateMachine);
            gameStateMachine.OnActionSubmitted.AddListener(_action);
        }

        private void HandleActionSubmitted(GameStateMachine stateMachine)
        {
            stateMachine.SetGameState<ProcessActionsState>();
            stateMachine.OnActionSubmitted.RemoveListener(_action);
        }
    }
}