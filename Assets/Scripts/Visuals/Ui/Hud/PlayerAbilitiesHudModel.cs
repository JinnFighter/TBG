using System.Collections.Generic;
using Logic.Actions;
using Visuals.Characters;

namespace Visuals.Ui.Hud
{
    public class PlayerAbilitiesHudModel : IPlayerAbilitiesHudModel
    {
        public PlayerAbilitiesHudModel(CharacterModel characterModel, IActionSubmitter actionSubmitter)
        {
            PlayerActions = new List<IPlayerAbilityHudModel>();
            foreach (var abilityModel in characterModel.CharacterAbilitiesModel.Abilities)
                PlayerActions.Add(new PlayerAbilityHudModel(abilityModel, characterModel.CharacterDataModel.Id.Value, 1,
                    actionSubmitter));
        }

        public List<IPlayerAbilityHudModel> PlayerActions { get; }
    }
}