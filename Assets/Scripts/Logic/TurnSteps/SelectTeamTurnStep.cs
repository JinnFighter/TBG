using Logic.Characters;

namespace Logic.TurnSteps
{
    public class SelectTeamTurnStep : BaseTurnStep
    {
        private readonly CharactersContainer _charactersContainer;

        public SelectTeamTurnStep(CharactersContainer charactersContainer)
        {
            _charactersContainer = charactersContainer;
        }

        public override ETurnStep Id => ETurnStep.SelectingTeam;

        protected override void DoStepInner(TurnContext context)
        {
            _charactersContainer.SwitchCurrentTeam();
            context.ActionInfo = null;
            context.ActionResult = null;
            MarkAsComplete();
        }
    }
}