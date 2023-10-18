using UnityEngine.Events;

namespace Logic.TurnSteps
{
    public class VisualizeActionsTurnStep : BaseTurnStep
    {
        private readonly UnityAction _action;
        private readonly IVisualizerService _visualizerService;

        public VisualizeActionsTurnStep(IVisualizerService visualizerService)
        {
            _visualizerService = visualizerService;
            _action = HandleVisualizationFinished;
        }

        public override ETurnStep Id => ETurnStep.VisualizeActions;

        protected override void EnterStepInner(TurnContext turnContext)
        {
            _visualizerService.OnVisualizeFinished.AddListener(_action);
        }

        protected override void DoStepInner(TurnContext turnContext)
        {
            _visualizerService.VisualizeAction(turnContext.ActionInfo, turnContext.ActionResult);
        }

        protected override void ExitStepInner(TurnContext context)
        {
            _visualizerService.OnVisualizeFinished.RemoveListener(_action);
        }

        private void HandleVisualizationFinished()
        {
            MarkAsComplete();
        }
    }
}