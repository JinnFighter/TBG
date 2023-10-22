using Logic.Characters;
using Logic.Config;

namespace Logic.Actions.ActionLogic
{
    public class DamageActionLogic : BaseActionLogic
    {
        private readonly AttackAbilityConfig _attackAbilityConfig;
        private readonly CharactersContainer _charactersContainer;

        public DamageActionLogic(CharactersContainer charactersContainer, AttackAbilityConfig attackAbilityConfig)
        {
            _charactersContainer = charactersContainer;
            _attackAbilityConfig = attackAbilityConfig;
        }

        protected override string ActionId => _attackAbilityConfig.Id;

        protected override void DoActionInner(ActionInfo actionInfo, ActionResultContainer actionResultContainer)
        {
            var target = _charactersContainer.Characters[actionInfo.TargetId];
            var oldHp = target.CharacterStats.Health;
            var damage = _attackAbilityConfig.Damage;
            target.CharacterStats.Damage(damage);
            var newHp = target.CharacterStats.Health;
            actionResultContainer.RegisterResult(new AttackActionResult
            {
                OriginalHealth = oldHp,
                Damage = damage,
                NewHealth = newHp
            });
        }
    }
}