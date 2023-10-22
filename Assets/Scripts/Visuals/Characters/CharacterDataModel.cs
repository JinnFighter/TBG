using Logic.Characters;
using Reactivity;

namespace Visuals.Characters
{
    public class CharacterDataModel : ICharacterDataModel
    {
        public CharacterDataModel(CharacterData characterData)
        {
            Id = new ReactiveProperty<int>(characterData.Id);
            Name = new ReactiveProperty<string>(characterData.Name);
            TeamId = new ReactiveProperty<ECharacterTeam>(characterData.TeamId);
        }

        public IReactiveProperty<int> Id { get; }
        public IReactiveProperty<string> Name { get; }
        public IReactiveProperty<ECharacterTeam> TeamId { get; }
    }
}