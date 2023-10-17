using Logic.Characters;
using Logic.TurnSteps;

namespace Logic
{
    public class SelectTeamGameStep : BaseGameStep
    {
        private readonly CharactersContainer _charactersContainer;

        public SelectTeamGameStep(CharactersContainer charactersContainer)
        {
            _charactersContainer = charactersContainer;
        }

        public override EGameStep Id => EGameStep.SelectingTeam;

        protected override void DoStepInner(TurnContext context)
        {
            _charactersContainer.SwitchCurrentTeam();
            context.ActionInfo = null;
            context.ActionResult = null;
            MarkAsComplete();
        }
    }
}