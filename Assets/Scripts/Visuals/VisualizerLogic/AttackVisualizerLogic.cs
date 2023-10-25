using Logic.Actions;
using Visuals.Characters;

namespace Visuals.VisualizerLogic
{
    public class AttackVisualizerLogic : IVisualizerLogic
    {
        private readonly BattleCharactersModel _battleCharactersModel;

        public AttackVisualizerLogic(BattleCharactersModel battleCharactersModel)
        {
            _battleCharactersModel = battleCharactersModel;
        }

        public void VisualizeAction(ActionInfo actionInfo, ActionResultContainer actionResultContainer)
        {
            var results = actionResultContainer.GetActionResults();
            foreach (var actionResult in results)
                if (actionResult is AttackActionResult attackActionResult)
                    _battleCharactersModel.CharacterModels[actionInfo.TargetId].CharacterStatsModel
                        .Damage(attackActionResult.Damage);
        }
    }
}