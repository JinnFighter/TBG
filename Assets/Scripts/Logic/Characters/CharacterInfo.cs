namespace Logic.Characters
{
    public class CharacterInfo
    {
        public readonly CharacterStats CharacterStats;
        public readonly int Id;
        public readonly string Name;
        public readonly int TeamId;

        public CharacterInfo(int id, string name, int teamId, CharacterStats characterStats)
        {
            Id = id;
            Name = name;
            TeamId = teamId;
            CharacterStats = characterStats;
        }

        public bool IsBot => TeamId == CharactersContainer.PlayerTeamId;
    }
}