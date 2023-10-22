using Reactivity;

namespace Visuals.Ui.Hud
{
    public class CharacterStatsModel : ICharacterStatsModel
    {
        private readonly ReactiveProperty<string> _characterName;
        private readonly ReactiveProperty<int> _currentHealth;
        private readonly ReactiveProperty<int> _maxHealth;

        public CharacterStatsModel(string characterName, int currentHealth, int maxHealth)
        {
            _characterName = new ReactiveProperty<string>(characterName);
            _currentHealth = new ReactiveProperty<int>(currentHealth);
            _maxHealth = new ReactiveProperty<int>(maxHealth);
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