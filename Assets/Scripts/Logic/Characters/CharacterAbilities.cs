using System.Collections.Generic;
using Logic.Config;

namespace Logic.Characters
{
    public class CharacterAbilities
    {
        public List<BaseAbilityConfig> Abilities { get; }

        public CharacterAbilities(List<BaseAbilityConfig> abilities)
        {
            Abilities = abilities;
        }
    }
}
