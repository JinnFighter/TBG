using Logic.Characters;
using Reactivity;

namespace Visuals.Ui.TargetSelection
{
    public interface ITargetSelectorModel : IModel
    {
        IReactiveProperty<int> SelectedIndex { get; }
        IReactiveProperty<ECharacterTeam> SelectedTeam { get; }

        void MoveLeft();
        void MoveRight();
    }
}