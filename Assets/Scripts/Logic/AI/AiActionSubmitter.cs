using Logic.Actions;
using Logic.CharacterQueue;
using Logic.Characters;
using Logic.TurnSteps;

namespace Logic.AI
{
    public class AiActionSubmitter : IAiActionSubmitter
    {
        private readonly IActionSubmitter _actionSubmitter;
        private readonly ICharacterQueue _characterQueue;
        private readonly GameStateMachine _gameStateMachine;

        public AiActionSubmitter(GameStateMachine gameStateMachine, IActionSubmitter actionSubmitter,
            ICharacterQueue characterQueue)
        {
            _gameStateMachine = gameStateMachine;
            _actionSubmitter = actionSubmitter;
            _characterQueue = characterQueue;
        }

        public void Init()
        {
            _gameStateMachine.OnStateEnter.AddListener(HandleStateChanged);
        }

        public void Terminate()
        {
            _gameStateMachine.OnStateEnter.RemoveListener(HandleStateChanged);
        }

        private void HandleStateChanged(ETurnStep step)
        {
            if (step != ETurnStep.AwaitingInput || _characterQueue.CurrentTeamId != ECharacterTeam.Enemy) return;

            _actionSubmitter.SubmitAction(new ActionInfo
            {
                ActionId = "attack",
                CasterId = _characterQueue.CurrentActiveCharacter,
                TargetId = 0
            });
        }
    }
}