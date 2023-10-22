using Logic.CharacterQueue;

namespace Logic.TurnSteps
{
    public class SelectTeamTurnStep : BaseTurnStep
    {
        private readonly ICharacterQueue _characterQueue;

        public SelectTeamTurnStep(ICharacterQueue characterQueue)
        {
            _characterQueue = characterQueue;
        }

        public override ETurnStep Id => ETurnStep.SelectingTeam;

        protected override void DoStepInner(TurnContext context)
        {
            _characterQueue.UpdateNextActiveCharacter();
            context.ActionInfo = null;
            context.ActionResult = null;
            MarkAsComplete();
        }
    }
}