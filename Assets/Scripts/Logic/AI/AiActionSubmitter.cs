using Logic.Actions;
using Logic.BattleService;
using Logic.CharacterQueue;
using Logic.Characters;
using Logic.Config;
using Logic.TurnSteps;

namespace Logic.AI
{
    public class AiActionSubmitter : IAiActionSubmitter
    {
        private readonly IActionSubmitter _actionSubmitter;
        private readonly AttackAbilityConfig _attackAbilityConfig;
        private readonly ICharacterQueue _characterQueue;
        private readonly IBattleService _battleService;

        public AiActionSubmitter(IBattleService battleService, IActionSubmitter actionSubmitter,
            ICharacterQueue characterQueue, AttackAbilityConfig attackAbilityConfig)
        {
            _battleService = battleService;
            _actionSubmitter = actionSubmitter;
            _characterQueue = characterQueue;
            _attackAbilityConfig = attackAbilityConfig;
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

            _actionSubmitter.SubmitAction(new ActionInfo
            {
                ActionId = _attackAbilityConfig.Id,
                CasterId = _characterQueue.CurrentActiveCharacter,
                TargetId = 0
            });
        }
    }
}