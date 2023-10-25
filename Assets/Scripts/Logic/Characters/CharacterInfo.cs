using Logic.Config;

namespace Logic.Characters
{
    public class CharacterInfo
    {
        public readonly CharacterAbilities CharacterAbilities;
        public readonly CharacterData CharacterData;
        public readonly CharacterStats CharacterStats;

        public CharacterInfo(CharacterConfig characterConfig, ECharacterTeam team)
        {
            CharacterData = new CharacterData((int) characterConfig.Id, characterConfig.Name, team);
            CharacterStats = new CharacterStats(10, 10);
            CharacterAbilities = new CharacterAbilities(characterConfig.Abilities);
        }

        public bool IsBot => CharacterData.TeamId == ECharacterTeam.Player;
    }
}