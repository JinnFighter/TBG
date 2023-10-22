using System.Collections.Generic;

namespace Logic.CharacterQueue
{
    public interface ICharacterQueue
    {
        int CurrentTeamId { get; }
        int CurrentActiveCharacter { get; }
        void Init(IEnumerable<int> characterIds);
        void Terminate();
        void UpdateNextActiveCharacter();
        void RemoveCharacterFromQueue(int id);
    }
}