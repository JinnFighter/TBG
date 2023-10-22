using Logic.Actions;
using Visuals.Characters;

namespace Visuals.Ui.Hud
{
    public class HudModel : IModel
    {
        public HudModel(CharacterModel characterModel, IActionSubmitter actionSubmitter)
        {
            PlayerAbilitiesHudModel = new PlayerAbilitiesHudModel(characterModel, actionSubmitter);
            CharacterStatsModel = characterModel.CharacterStatsModel;
        }

        public IPlayerAbilitiesHudModel PlayerAbilitiesHudModel { get; }
        public ICharacterStatsModel CharacterStatsModel { get; }
    }
}