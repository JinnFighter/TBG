using UnityEngine;

namespace Visuals.BattleArena
{
    public class BattleArenaSceneData : MonoBehaviour
    {
        [field: SerializeField] public CharacterSpawnSlotsView CharacterSpawnSlotsView { get; private set; }
    }
}