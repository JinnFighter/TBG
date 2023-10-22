using Logic;
using Logic.Actions;
using Logic.Config;
using Visuals.Characters;

namespace Visuals.VisualizerLogic
{
    public class AttackVisualizerLogic : IVisualizerLogic
    {
        private readonly BattleCharactersModel _battleCharactersModel;
        private readonly AttackAbilityConfig _attackAbilityConfig;

        public AttackVisualizerLogic(BattleCharactersModel battleCharactersModel, AttackAbilityConfig attackAbilityConfig)
        {
            _battleCharactersModel = battleCharactersModel;
            _attackAbilityConfig = attackAbilityConfig;
        }

        public void VisualizeAction(ActionInfo actionInfo, ActionResultContainer actionResultContainer)
        {
            var results = actionResultContainer.GetActionResults();
            foreach (var actionResult in results)
                if (actionInfo.ActionId == _attackAbilityConfig.Id && actionResult is AttackActionResult attackActionResult)
                    _battleCharactersModel.CharacterModels[actionInfo.TargetId].CharacterStatsModel
                        .Damage(attackActionResult.Damage);
        }
    }
}