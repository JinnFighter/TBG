using UnityEngine;

namespace Logic.Config
{
    [CreateAssetMenu(menuName = "Create AttackAbilityConfig", fileName = "AttackAbilityConfig", order = 0)]
    public class AttackAbilityConfig : BaseAbilityConfig
    {
        [field: SerializeField] public int Damage { get; private set; }
    }
}