using Logic.Actions;

namespace Visuals.VisualizerLogic
{
    public interface IVisualizerLogic
    {
        void VisualizeAction(ActionInfo actionInfo, ActionResultContainer actionResultContainer);
    }
}