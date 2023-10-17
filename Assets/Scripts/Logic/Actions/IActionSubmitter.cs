using UnityEngine.Events;

namespace Logic.Actions
{
    public interface IActionSubmitter
    {
        UnityEvent<ActionInfo> OnActionSubmitted { get; }
        void SubmitAction(ActionInfo actionInfo);
    }
}