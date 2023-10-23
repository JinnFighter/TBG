using Logic.Actions;
using Logic.Config;
using Reactivity;

namespace Visuals.Characters
{
    public class CharacterAbilityModel : ICharacterAbilityModel
    {
        private readonly IActionSubmitter _actionSubmitter;

        public CharacterAbilityModel(BaseAbilityConfig abilityConfig, IActionSubmitter actionSubmitter)
        {
            Id = new ReactiveProperty<string>(abilityConfig.Id);
            Name = new ReactiveProperty<string>(abilityConfig.Name);
            _actionSubmitter = actionSubmitter;
        }

        public IReactiveProperty<string> Id { get; }
        public IReactiveProperty<string> Name { get; }

        public void SubmitAction(int casterId, int targetId)
        {
            _actionSubmitter.SubmitAction(new ActionInfo
            {
                ActionId = Id.Value,
                CasterId = casterId,
                TargetId = targetId
            });
        }
    }
}