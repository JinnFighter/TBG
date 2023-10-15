using UnityEngine;

namespace Logic
{
    public class VisualizeActionsState : IGameState
    {
        public EGameState Id => EGameState.VisualizeActions;

        public void EnterState(GameStateMachine gameStateMachine)
        {
            Debug.Log($"Enter state: {Id}");
            gameStateMachine.SetGameState<SelectTeamGameState>();
        }
    }
}