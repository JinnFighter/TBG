using System.Collections.Generic;
using Logic.Actions.ActionLogic;
using Logic.Characters;

namespace Logic.Actions
{
    public class ActionProcessor : IActionProcessor
    {
        private readonly List<IActionLogic> _actionLogics = new();
        private readonly CharactersContainer _charactersContainer;

        public ActionProcessor(CharactersContainer charactersContainer)
        {
            _charactersContainer = charactersContainer;
        }

        public void Init()
        {
            _actionLogics.Add(new DamageActionLogic(_charactersContainer));
        }

        public void Terminate()
        {
            _actionLogics.Clear();
        }

        public ActionResultContainer ProcessAction(ActionInfo actionInfo)
        {
            var resultContainer = new ActionResultContainer();
            foreach (var actionLogic in _actionLogics) actionLogic.DoAction(actionInfo, resultContainer);
            return resultContainer;
        }
    }
}