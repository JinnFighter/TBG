using UnityEngine;

namespace Logic
{
    public class AwaitingInputGameState : IGameState
    {
        public EGameState Id => EGameState.AwaitingInput;

        public void EnterState(GameStateMachine gameStateMachine)
        {
            Debug.Log($"Enter state: {Id}");
        }
    }
}