using System.Collections.Generic;

namespace Visuals.BattleArena
{
    public class CharacterSpawnSlotsModel
    {
        private readonly Queue<int> _freeSlots;

        public CharacterSpawnSlotsModel(int slotsCount)
        {
            Slots = new List<int>();
            _freeSlots = new Queue<int>();
            for (var i = 0; i < slotsCount; i++)
            {
                Slots.Add(-1);
                _freeSlots.Enqueue(i);
            }
        }

        public List<int> Slots { get; }

        public int SpawnAtSlot(int characterId)
        {
            if (_freeSlots.Count > 0)
            {
                var slotId = _freeSlots.Dequeue();
                Slots[slotId] = characterId;
                return slotId;
            }

            return -1;
        }

        public void RemoveAtSlot(int slotId)
        {
            if (Slots[slotId] != -1)
            {
                _freeSlots.Enqueue(slotId);
                Slots[slotId] = -1;
            }
        }
    }
}