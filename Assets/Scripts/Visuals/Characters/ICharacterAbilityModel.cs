using Reactivity;

namespace Visuals.Characters
{
    public interface ICharacterAbilityModel : IModel
    {
        IReactiveProperty<string> Id { get; }
        IReactiveProperty<string> Name { get; }
    }
}