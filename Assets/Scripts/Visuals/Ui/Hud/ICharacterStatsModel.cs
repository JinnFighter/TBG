using Reactivity;

namespace Visuals.Ui.Hud
{
    public interface ICharacterStatsModel : IModel
    {
        IReactiveProperty<string> CharacterName { get; }
        IReactiveProperty<int> CurrentHealth { get; }
        IReactiveProperty<int> MaxHealth { get; }
        void Heal(int amount);
        void Damage(int amount);
    }
}