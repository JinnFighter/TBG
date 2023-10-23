using System.Collections.Generic;
using Logic.Config;

namespace Logic.Characters
{
    public class CharacterAbilities
    {
        private readonly Dictionary<string, BaseAbilityConfig> _abilitiesDictAlias = new();

        public CharacterAbilities(List<BaseAbilityConfig> abilities)
        {
            Abilities = abilities;
            foreach (var ability in abilities) _abilitiesDictAlias.Add(ability.Id, ability);
        }

        public List<BaseAbilityConfig> Abilities { get; }

        public T GetAbility<T>(string id) where T : BaseAbilityConfig
        {
            return _abilitiesDictAlias[id] as T;
        }
    }
}