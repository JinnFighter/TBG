namespace Logic.Characters
{
    public class CharacterInfo
    {
        public readonly CharacterStats CharacterStats;
        public readonly int Id;
        public readonly string Name;
        public readonly ECharacterTeam TeamId;

        public CharacterInfo(int id, string name, ECharacterTeam teamId, CharacterStats characterStats)
        {
            Id = id;
            Name = name;
            TeamId = teamId;
            CharacterStats = characterStats;
        }

        public bool IsBot => TeamId == ECharacterTeam.Player;
    }
}