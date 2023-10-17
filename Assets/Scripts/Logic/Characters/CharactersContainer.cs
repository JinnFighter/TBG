using System.Collections.Generic;
using UnityEngine;

namespace Logic.Characters
{
    public class CharactersContainer
    {
        public const int PlayerTeamId = 0;
        public const int EnemyTeamId = 1;
        public int CurrentTeamId { get; private set; } = -1;
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

        public void SwitchCurrentTeam()
        {
            CurrentTeamId = CurrentTeamId switch
            {
                -1 => PlayerTeamId,
                0 => EnemyTeamId,
                _ => PlayerTeamId
            };

            Debug.Log($"Current Team : {CurrentTeamId}");
        }
    }
}