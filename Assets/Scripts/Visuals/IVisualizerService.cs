using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Logic;
using Logic.Actions;
using UnityEngine.Events;
using Visuals.VisualizerLogic;

namespace Visuals
{
    public interface IVisualizerService
    {
        UnityEvent OnVisualizeStarted { get; }
        UnityEvent OnVisualizeFinished { get; }
        bool IsVisualizing { get; }

        void Init(List<IVisualizerLogic> logics);
        void Terminate();
        UniTask VisualizeAction(ActionInfo actionInfo, ActionResultContainer actionResultContainer);
    }
}