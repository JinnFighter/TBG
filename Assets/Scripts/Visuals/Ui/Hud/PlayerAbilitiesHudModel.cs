using System.Collections.Generic;
using Visuals.Characters;

namespace Visuals.Ui.Hud
{
    public class PlayerAbilitiesHudModel : IPlayerAbilitiesHudModel
    {
        public PlayerAbilitiesHudModel(CharacterModel characterModel)
        {
            PlayerActions = new List<IPlayerAbilityHudModel>();
            foreach (var abilityModel in characterModel.CharacterAbilitiesModel.Abilities)
                PlayerActions.Add(
                    new PlayerAbilityHudModel(abilityModel, characterModel.CharacterDataModel.Id.Value, 1));
        }

        public List<IPlayerAbilityHudModel> PlayerActions { get; }
    }
}