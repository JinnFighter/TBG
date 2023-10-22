using System.Linq;
using Logic.Characters;
using UnityEngine;

namespace Logic.TurnSteps
{
    public class CheckTurnOverTurnStep : BaseTurnStep
    {
        private readonly CharactersContainer _charactersContainer;

        public CheckTurnOverTurnStep(CharactersContainer charactersContainer)
        {
            _charactersContainer = charactersContainer;
        }

        public override ETurnStep Id => ETurnStep.CheckGameOver;

        protected override void DoStepInner(TurnContext turnContext)
        {
            if (_charactersContainer.CharacterTeams[CharacterConst.PlayerTeamId]
                .All(character => character.CharacterStats.Health <= 0))
                Debug.Log("Game over => enemy wins!");
            else if (_charactersContainer.CharacterTeams[CharacterConst.EnemyTeamId]
                     .All(character => character.CharacterStats.Health <= 0))
                Debug.Log("Game over => player wins!");
            else
                MarkAsComplete();
        }
    }
}