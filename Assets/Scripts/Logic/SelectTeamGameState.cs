using UnityEngine;

namespace Logic
{
    public class SelectTeamGameState : IGameState
    {
        public EGameState Id => EGameState.SelectingTeam;

        public void EnterState(GameStateMachine gameStateMachine)
        {
            Debug.Log($"Enter state: {Id}");
            gameStateMachine.SetGameState<AwaitingInputGameState>();
        }
    }
}