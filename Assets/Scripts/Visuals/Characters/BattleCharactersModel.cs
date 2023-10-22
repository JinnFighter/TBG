using System.Collections.Generic;
using System.Linq;
using Logic.Characters;
using Visuals.Ui.Hud;

namespace Visuals.Characters
{
    public class BattleCharactersModel
    {
        public BattleCharactersModel(Dictionary<int, CharacterInfo> characterInfos)
        {
            CharacterModels = new Dictionary<int, CharacterModel>();

            foreach (var characterInfo in characterInfos.Keys.Select(k => characterInfos[k]))
                CharacterModels.Add(characterInfo.CharacterData.Id, new CharacterModel(
                    new CharacterDataModel(characterInfo.CharacterData),
                    new CharacterStatsModel(characterInfo),
                    new CharacterAbilitiesModel(characterInfo.CharacterAbilities)));
        }

        public Dictionary<int, CharacterModel> CharacterModels { get; }
    }
}