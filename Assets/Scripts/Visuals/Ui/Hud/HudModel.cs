using Visuals.Characters;

namespace Visuals.Ui.Hud
{
    public class HudModel : IModel
    {
        public HudModel(CharacterModel characterModel)
        {
            PlayerAbilitiesHudModel = new PlayerAbilitiesHudModel(characterModel);
            CharacterStatsModel = characterModel.CharacterStatsModel;
        }

        public IPlayerAbilitiesHudModel PlayerAbilitiesHudModel { get; }
        public ICharacterStatsModel CharacterStatsModel { get; }
    }
}