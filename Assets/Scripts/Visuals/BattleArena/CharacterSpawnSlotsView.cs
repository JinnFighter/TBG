using System.Collections.Generic;
using UnityEngine;

namespace Visuals.BattleArena
{
    public class CharacterSpawnSlotsView : BaseView
    {
        [field: SerializeField] public List<CharacterSpawnSlotView> PlayerTeamSpawnSlots { get; private set; }
        [field: SerializeField] public List<CharacterSpawnSlotView> EnemyTeamSpawnSlots { get; private set; }
    }
}