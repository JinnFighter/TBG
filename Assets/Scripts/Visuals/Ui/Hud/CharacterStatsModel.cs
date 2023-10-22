using Logic.Characters;
using Reactivity;

namespace Visuals.Ui.Hud
{
    public class CharacterStatsModel : ICharacterStatsModel
    {
        private readonly ReactiveProperty<string> _characterName;
        private readonly ReactiveProperty<int> _currentHealth;
        private readonly ReactiveProperty<int> _maxHealth;

        public CharacterStatsModel(CharacterInfo characterInfo)
        {
            _characterName = new ReactiveProperty<string>(characterInfo.CharacterData.Name);
            _currentHealth = new ReactiveProperty<int>(characterInfo.CharacterStats.Health);
            _maxHealth = new ReactiveProperty<int>(characterInfo.CharacterStats.MaxHealth);
        }

        public IReactiveProperty<string> CharacterName => _characterName;
        public IReactiveProperty<int> CurrentHealth => _currentHealth;
        public IReactiveProperty<int> MaxHealth => _maxHealth;

        public void Heal(int amount)
        {
            _currentHealth.Value += amount;
        }

        public void Damage(int amount)
        {
            _currentHealth.Value -= amount;
        }
    }
}