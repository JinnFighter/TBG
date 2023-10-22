using System.Collections.Generic;
using Logic.Characters;

namespace Logic.CharacterQueue
{
    public interface ICharacterQueue
    {
        ECharacterTeam CurrentTeamId { get; }
        int CurrentActiveCharacter { get; }
        void Init(IEnumerable<int> characterIds);
        void Terminate();
        void UpdateNextActiveCharacter();
        void RemoveCharacterFromQueue(int id);
    }
}