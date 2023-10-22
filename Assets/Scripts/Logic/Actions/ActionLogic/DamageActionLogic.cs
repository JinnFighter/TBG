using Logic.Characters;

namespace Logic.Actions.ActionLogic
{
    public class DamageActionLogic : BaseActionLogic
    {
        private readonly CharactersContainer _charactersContainer;

        public DamageActionLogic(CharactersContainer charactersContainer)
        {
            _charactersContainer = charactersContainer;
        }

        protected override string ActionId => "attack";

        protected override void DoActionInner(ActionInfo actionInfo, ActionResultContainer actionResultContainer)
        {
            var target = _charactersContainer.Characters[actionInfo.TargetId];
            var oldHp = target.CharacterStats.Health;
            var damage = 5;
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