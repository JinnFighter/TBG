using System.Collections.Generic;
using Logic.Characters;

namespace Visuals.Characters
{
    public class CharacterAbilitiesModel : ICharacterAbilitiesModel
    {
        public CharacterAbilitiesModel(CharacterAbilities characterAbilities)
        {
            Abilities = new List<ICharacterAbilityModel>();
            foreach (var config in characterAbilities.Abilities) Abilities.Add(new CharacterAbilityModel(config));
        }

        public List<ICharacterAbilityModel> Abilities { get; }
    }
}