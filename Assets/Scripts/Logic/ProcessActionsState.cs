using UnityEngine;

namespace Logic
{
    public class ProcessActionsState : IGameState
    {
        public EGameState Id => EGameState.ProcessActions;

        public void EnterState(GameStateMachine gameStateMachine)
        {
            Debug.Log($"Enter state: {Id}");
            gameStateMachine.ProcessAction();
            gameStateMachine.SetGameState<VisualizeActionsState>();
        }
    }
}