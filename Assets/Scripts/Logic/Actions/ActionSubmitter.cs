using UnityEngine.Events;

namespace Logic.Actions
{
    public class ActionSubmitter : IActionSubmitter
    {
        public UnityEvent<ActionInfo> OnActionSubmitted { get; } = new();

        public void SubmitAction(ActionInfo actionInfo)
        {
            if (actionInfo == null) return;
            OnActionSubmitted.Invoke(actionInfo);
        }
    }
}