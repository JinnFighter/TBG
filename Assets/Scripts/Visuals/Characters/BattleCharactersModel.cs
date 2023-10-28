using System.Collections.Generic;
using System.Linq;
using Logic.Actions;
using Logic.Characters;
using Visuals.Ui.Hud;

namespace Visuals.Characters
{
    public class BattleCharactersModel
    {
        public BattleCharactersModel(Dictionary<int, CharacterInfo> characterInfos, IActionSubmitter actionSubmitter)
        {
            CharacterModels = new Dictionary<int, CharacterModel>();
            TeamCharacterModels = new Dictionary<ECharacterTeam, List<CharacterModel>>();

            foreach (var characterInfo in characterInfos.Keys.Select(k => characterInfos[k]))
            {
                if (!TeamCharacterModels.TryGetValue(characterInfo.CharacterData.TeamId, out _))
                    TeamCharacterModels[characterInfo.CharacterData.TeamId] = new List<CharacterModel>();

                var model = new CharacterModel(
                    new CharacterDataModel(characterInfo.CharacterData),
                    new CharacterStatsModel(characterInfo),
                    new CharacterAbilitiesModel(characterInfo.CharacterAbilities, actionSubmitter));
                CharacterModels.Add(characterInfo.CharacterData.Id, model);
                TeamCharacterModels[characterInfo.CharacterData.TeamId].Add(model);
            }
        }

        public Dictionary<int, CharacterModel> CharacterModels { get; }
        public Dictionary<ECharacterTeam, List<CharacterModel>> TeamCharacterModels { get; }
    }
}