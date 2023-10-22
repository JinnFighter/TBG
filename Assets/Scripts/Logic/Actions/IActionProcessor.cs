using System.Collections.Generic;
using Logic.Actions.ActionLogic;

namespace Logic.Actions
{
    public interface IActionProcessor
    {
        void Init(List<IActionLogic> logics);
        void Terminate();
        ActionResultContainer ProcessAction(ActionInfo actionInfo);
    }
}