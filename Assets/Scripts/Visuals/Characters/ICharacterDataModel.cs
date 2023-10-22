using Logic.Characters;
using Reactivity;

namespace Visuals.Characters
{
    public interface ICharacterDataModel : IModel
    {
        IReactiveProperty<int> Id { get; }
        IReactiveProperty<string> Name { get; }
        IReactiveProperty<ECharacterTeam> TeamId { get; }
    }
}