using Visuals.Ui.Hud;

namespace Visuals.Characters
{
    public class CharacterModel : IModel
    {
        public CharacterModel(ICharacterDataModel characterDataModel, ICharacterStatsModel characterStatsModel,
            ICharacterAbilitiesModel characterAbilitiesModel)
        {
            CharacterDataModel = characterDataModel;
            CharacterStatsModel = characterStatsModel;
            CharacterAbilitiesModel = characterAbilitiesModel;
        }

        public ICharacterDataModel CharacterDataModel { get; }
        public ICharacterStatsModel CharacterStatsModel { get; }
        public ICharacterAbilitiesModel CharacterAbilitiesModel { get; }
    }
}