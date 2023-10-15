using Logic.Actions;
using Logic.Characters;
using UnityEngine;
using UnityEngine.Events;

namespace Logic
{
    public class AwaitingInputGameState : IGameState
    {
        private UnityAction<ActionInfo> _action;
        public EGameState Id => EGameState.AwaitingInput;

        public void EnterState(GameStateMachine gameStateMachine)
        {
            Debug.Log($"Enter state: {Id}");

            gameStateMachine.ResetSubmittedAction();
            _action = _ => HandleActionSubmitted(gameStateMachine);
            gameStateMachine.OnActionSubmitted.AddListener(_action);

            if (gameStateMachine.CharactersContainer.CurrentTeamId == CharactersContainer.EnemyTeamId)
                gameStateMachine.TrySubmitAction(new ActionInfo
                {
                    ActionId = "test",
                    CasterId = 1
                });
        }

        private void HandleActionSubmitted(GameStateMachine stateMachine)
        {
            stateMachine.OnActionSubmitted.RemoveListener(_action);
            stateMachine.SetGameState<ProcessActionsState>();
        }
    }
}