using UnityEngine;

namespace Logic.Characters
{
    public class CharacterStats
    {
        public CharacterStats(int health, int maxHealth)
        {
            Health = health;
            MaxHealth = maxHealth;
        }

        public int Health { get; private set; }
        public int MaxHealth { get; }

        public void Heal(int hp)
        {
            Health = Mathf.Clamp(Health + hp, Health, MaxHealth);
        }

        public void Damage(int damage)
        {
            Health = Mathf.Clamp(Health - damage, 0, Health);
        }
    }
}