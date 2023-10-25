using System.Collections.Generic;
using UnityEngine;

namespace Logic.Config
{
    [CreateAssetMenu(menuName = "Create CharacterConfig", fileName = "CharacterConfig", order = 0)]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public ECharacterId Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public List<BaseAbilityConfig> Abilities { get; private set; }
    }

    public enum ECharacterId
    {
        Player,
        Enemy
    }
}