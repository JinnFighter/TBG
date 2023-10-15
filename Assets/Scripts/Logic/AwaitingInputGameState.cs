using UnityEngine;

namespace Logic
{
    public class AwaitingInputGameState : IGameState
    {
        public EGameState Id => EGameState.AwaitingInput;

        public void EnterState(GameStateMachine gameStateMachine)
        {
            Debug.Log($"Enter state: {Id}");

            gameStateMachine.ResetSubmittedAction();
            gameStateMachine.OnActionSubmitted.AddListener(_ => HandleActionSubmitted(gameStateMachine));
        }

        private void HandleActionSubmitted(GameStateMachine stateMachine)
        {
            stateMachine.SetGameState<ProcessActionsState>();
        }
    }
}