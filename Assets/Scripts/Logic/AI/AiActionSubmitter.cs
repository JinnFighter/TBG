using System.Linq;
using Logic.Actions;
using Logic.BattleService;
using Logic.CharacterQueue;
using Logic.Characters;
using Logic.TurnSteps;

namespace Logic.AI
{
    public class AiActionSubmitter : IAiActionSubmitter
    {
        private readonly IActionSubmitter _actionSubmitter;
        private readonly IBattleService _battleService;
        private readonly ICharacterQueue _characterQueue;
        private readonly CharactersContainer _charactersContainer;

        public AiActionSubmitter(IBattleService battleService, IActionSubmitter actionSubmitter,
            ICharacterQueue characterQueue, CharactersContainer charactersContainer)
        {
            _battleService = battleService;
            _actionSubmitter = actionSubmitter;
            _characterQueue = characterQueue;
            _charactersContainer = charactersContainer;
        }

        public void Init()
        {
            _battleService.OnTurnStepEnter.AddListener(HandleStateChanged);
        }

        public void Terminate()
        {
            _battleService.OnTurnStepEnter.RemoveListener(HandleStateChanged);
        }

        private void HandleStateChanged(ETurnStep step)
        {
            if (step != ETurnStep.AwaitingInput || _characterQueue.CurrentTeamId != ECharacterTeam.Enemy) return;

            var curActiveCharacterId = _characterQueue.CurrentActiveCharacter;
            var activeCharacter = _charactersContainer.Characters[curActiveCharacterId];
            _actionSubmitter.SubmitAction(new ActionInfo
            {
                ActionId = activeCharacter.CharacterAbilities.Abilities.First().Id,
                CasterId = curActiveCharacterId,
                TargetId = 0
            });
        }
    }
}