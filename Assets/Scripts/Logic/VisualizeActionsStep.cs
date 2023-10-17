using Logic.TurnSteps;
using UnityEngine.Events;

namespace Logic
{
    public class VisualizeActionsStep : BaseGameStep
    {
        private readonly UnityAction _action;
        private readonly IVisualizerService _visualizerService;

        public VisualizeActionsStep(IVisualizerService visualizerService)
        {
            _visualizerService = visualizerService;
            _action = HandleVisualizationFinished;
        }

        public override EGameStep Id => EGameStep.VisualizeActions;

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