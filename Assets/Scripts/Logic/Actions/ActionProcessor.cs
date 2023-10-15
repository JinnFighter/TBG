using System.Collections.Generic;
using UnityEngine;

namespace Logic.Actions
{
    public class ActionProcessor
    {
        private readonly List<IActionLogic> _actionLogics = new();

        public void Init()
        {
            _actionLogics.Add(new TestActionLogic());
        }

        public void Terminate()
        {
            _actionLogics.Clear();
        }

        public void ProcessAction(ActionInfo actionInfo)
        {
            foreach (var actionLogic in _actionLogics) actionLogic.DoAction(actionInfo);
        }
    }

    public interface IActionLogic
    {
        void DoAction(ActionInfo actionInfo);
    }

    public class TestActionLogic : IActionLogic
    {
        public void DoAction(ActionInfo actionInfo)
        {
            Debug.Log($"Action {actionInfo.ActionId} was casted by entity {actionInfo.CasterId}");
        }
    }
}