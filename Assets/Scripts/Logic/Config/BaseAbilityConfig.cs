using UnityEngine;

namespace Logic.Config
{
    public class BaseAbilityConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    }
}