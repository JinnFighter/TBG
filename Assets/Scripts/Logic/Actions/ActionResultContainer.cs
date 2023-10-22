using System.Collections.Generic;

namespace Logic.Actions
{
    public class ActionResultContainer
    {
        private readonly List<IActionResult> _actionResults = new();

        public void RegisterResult(IActionResult actionResult)
        {
            _actionResults.Add(actionResult);
        }

        public List<IActionResult> GetActionResults()
        {
            return _actionResults;
        }
    }
}