using Logic.Config;
using Reactivity;

namespace Visuals.Characters
{
    public class CharacterAbilityModel : ICharacterAbilityModel
    {
        public CharacterAbilityModel(BaseAbilityConfig abilityConfig)
        {
            Id = new ReactiveProperty<string>(abilityConfig.Id);
            Name = new ReactiveProperty<string>(abilityConfig.Name);
        }

        public IReactiveProperty<string> Id { get; }
        public IReactiveProperty<string> Name { get; }
    }
}