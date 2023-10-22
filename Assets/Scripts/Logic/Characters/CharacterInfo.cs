namespace Logic.Characters
{
    public class CharacterInfo
    {
        public readonly CharacterAbilities CharacterAbilities;
        public readonly CharacterData CharacterData;
        public readonly CharacterStats CharacterStats;

        public CharacterInfo(CharacterData characterData, CharacterStats characterStats,
            CharacterAbilities characterAbilities)
        {
            CharacterData = characterData;
            CharacterStats = characterStats;
            CharacterAbilities = characterAbilities;
        }

        public bool IsBot => CharacterData.TeamId == ECharacterTeam.Player;
    }
}