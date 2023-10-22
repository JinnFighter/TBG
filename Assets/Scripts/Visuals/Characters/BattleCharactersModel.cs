using System.Collections.Generic;
using Logic.Characters;
using Visuals.Ui.Hud;

namespace Visuals.Characters
{
    public class BattleCharactersModel
    {
        public BattleCharactersModel(Dictionary<int, CharacterInfo> characterInfos)
        {
            CharacterModels = new Dictionary<int, CharacterModel>();

            foreach (var k in characterInfos.Keys)
            {
                var characterInfo = characterInfos[k];
                CharacterModels.Add(characterInfo.Id, new CharacterModel(characterInfo.TeamId, new CharacterStatsModel(
                    characterInfo.Name,
                    characterInfo.CharacterStats.Health, characterInfo.CharacterStats.MaxHealth)));
            }
        }

        public Dictionary<int, CharacterModel> CharacterModels { get; }
    }
}