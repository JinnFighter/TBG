using Logic.Characters;
using Logic.Config;

namespace Logic.Actions.ActionLogic
{
    public class DamageActionLogic : BaseActionLogic
    {
        private readonly CharactersContainer _charactersContainer;

        public DamageActionLogic(CharactersContainer charactersContainer)
        {
            _charactersContainer = charactersContainer;
        }

        protected override void DoActionInner(ActionInfo actionInfo, ActionResultContainer actionResultContainer)
        {
            var caster = _charactersContainer.Characters[actionInfo.CasterId];
            var target = _charactersContainer.Characters[actionInfo.TargetId];
            var oldHp = target.CharacterStats.Health;
            var attackAbility = caster.CharacterAbilities.GetAbility<AttackAbilityConfig>(actionInfo.ActionId);
            var damage = attackAbility.Damage;
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