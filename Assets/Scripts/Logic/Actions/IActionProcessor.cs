using System.Collections.Generic;
using Logic.Actions.ActionLogic;
using UnityEngine.Events;

namespace Logic.Actions
{
    public interface IActionProcessor
    {
        void Init(List<IActionLogic> logics);
        void Terminate();
        ActionResultContainer ProcessAction(ActionInfo actionInfo);
        UnityEvent<ActionInfo, ActionResultContainer> OnActionProcessingFinished { get; }
    }
}