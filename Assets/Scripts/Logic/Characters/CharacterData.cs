namespace Logic.Characters
{
    public class CharacterData
    {
        public CharacterData(int id, string name, ECharacterTeam teamId)
        {
            Id = id;
            Name = name;
            TeamId = teamId;
        }

        public int Id { get; }
        public string Name { get; }
        public ECharacterTeam TeamId { get; }
    }
}