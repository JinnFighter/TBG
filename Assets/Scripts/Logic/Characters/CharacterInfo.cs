namespace Logic.Characters
{
    public class CharacterInfo
    {
        public readonly int Id;
        public readonly string Name;
        public readonly int TeamId;

        public CharacterInfo(int id, string name, int teamId)
        {
            Id = id;
            Name = name;
            TeamId = teamId;
        }

        public bool IsBot => TeamId == CharactersContainer.PlayerTeamId;
    }
}