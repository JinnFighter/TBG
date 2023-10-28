using Logic.Characters;
using Reactivity;
using Visuals.BattleArena;

namespace Visuals.Ui.TargetSelection
{
    public class TargetSelectorModel : ITargetSelectorModel
    {
        private readonly ReactiveProperty<int> _selectedIndex;
        private readonly CharacterSpawnSlotsModel _spawnSlots;

        public TargetSelectorModel(CharacterSpawnSlotsModel spawnSlots, ECharacterTeam team)
        {
            _spawnSlots = spawnSlots;
            SelectedTeam = new ReactiveProperty<ECharacterTeam>(team);
            _selectedIndex = new ReactiveProperty<int>(0);
        }

        public IReactiveProperty<int> SelectedIndex => _selectedIndex;
        public IReactiveProperty<ECharacterTeam> SelectedTeam { get; }

        public void MoveLeft()
        {
            var nextIndex = _selectedIndex.Value - 1;
            if (nextIndex < 0) nextIndex = _spawnSlots.Slots.Count - 1;

            _selectedIndex.Value = nextIndex;
        }

        public void MoveRight()
        {
            var nextIndex = _selectedIndex.Value + 1;
            if (nextIndex >= _spawnSlots.Slots.Count) nextIndex = 0;

            _selectedIndex.Value = nextIndex;
        }
    }
}