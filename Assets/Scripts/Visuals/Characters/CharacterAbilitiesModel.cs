using System.Collections.Generic;
using Logic.Actions;
using Logic.Characters;

namespace Visuals.Characters
{
    public class CharacterAbilitiesModel : ICharacterAbilitiesModel
    {
        public CharacterAbilitiesModel(CharacterAbilities characterAbilities, IActionSubmitter actionSubmitter)
        {
            Abilities = new List<ICharacterAbilityModel>();
            foreach (var config in characterAbilities.Abilities)
                Abilities.Add(new CharacterAbilityModel(config, actionSubmitter));
        }

        public List<ICharacterAbilityModel> Abilities { get; }
    }
}