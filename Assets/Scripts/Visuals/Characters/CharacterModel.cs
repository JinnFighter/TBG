using Logic.Characters;
using Reactivity;
using Visuals.Ui.Hud;

namespace Visuals.Characters
{
    public class CharacterModel : IModel
    {
        
        public CharacterModel(ECharacterTeam team, ICharacterStatsModel characterStatsModel)
        {
            Team = new ReactiveProperty<ECharacterTeam>(team);
            CharacterStatsModel = characterStatsModel;
        }

        public ICharacterStatsModel CharacterStatsModel { get; }
        public IReactiveProperty<ECharacterTeam> Team { get; }
    }
}