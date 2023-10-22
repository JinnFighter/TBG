using System.Collections.Generic;

namespace Logic.Characters
{
    public class CharactersContainer
    {
        public Dictionary<int, List<CharacterInfo>> CharacterTeams { get; } = new();
        public Dictionary<int, CharacterInfo> Characters { get; } = new();

        public void Init(List<CharacterInfo> characterInfos)
        {
            foreach (var characterInfo in characterInfos)
            {
                Characters.Add(characterInfo.Id, characterInfo);
                if (!CharacterTeams.TryGetValue(characterInfo.TeamId, out var teamChars))
                    CharacterTeams.Add(characterInfo.TeamId, new List<CharacterInfo>());

                CharacterTeams[characterInfo.TeamId].Add(characterInfo);
            }
        }

        public void Terminate()
        {
            Characters.Clear();
            CharacterTeams.Clear();
        }
    }
}