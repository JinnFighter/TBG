using System.Collections.Generic;
using Logic.Characters;
using UnityEngine;

namespace Logic.CharacterQueue
{
    public class CharacterQueue : ICharacterQueue
    {
        private readonly Dictionary<int, LinkedListNode<int>> _characterNodes = new();
        private readonly LinkedList<int> _queue = new();

        public int CurrentTeamId { get; private set; } = -1;
        public int CurrentActiveCharacter { get; private set; } = -1;

        public void Init(IEnumerable<int> characterIds)
        {
            foreach (var id in characterIds) _characterNodes[id] = _queue.AddLast(id);
        }

        public void Terminate()
        {
            _characterNodes.Clear();
            _queue.Clear();
            CurrentActiveCharacter = -1;
        }

        public void UpdateNextActiveCharacter()
        {
            if (CurrentActiveCharacter == -1)
                CurrentActiveCharacter = _queue.First.Value;
            else if (_characterNodes[CurrentActiveCharacter].Next == null)
                CurrentActiveCharacter = _queue.First.Value;
            else
                CurrentActiveCharacter = _characterNodes[CurrentActiveCharacter].Next.Value;

            CurrentTeamId = CurrentTeamId switch
            {
                -1 => CharacterConst.PlayerTeamId,
                0 => CharacterConst.EnemyTeamId,
                _ => CharacterConst.PlayerTeamId
            };

            Debug.Log($"Current Team : {CurrentTeamId}");
            Debug.Log($"Current Active Character : {CurrentActiveCharacter}");
        }

        public void RemoveCharacterFromQueue(int id)
        {
            _queue.Remove(_characterNodes[id]);
            _characterNodes.Remove(id);
        }
    }
}