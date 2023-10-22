using Logic.Actions;
using Reactivity;
using Visuals.Characters;

namespace Visuals.Ui.Hud
{
    public class PlayerAbilityHudModel : IPlayerAbilityHudModel
    {
        private readonly IActionSubmitter _actionSubmitter;
        private readonly ICharacterAbilityModel _abilityModel;
        private readonly int _casterId;
        private readonly int _targetId;

        public PlayerAbilityHudModel(ICharacterAbilityModel characterAbilityModel, int casterId, int targetId, IActionSubmitter actionSubmitter)
        {
            _abilityModel = characterAbilityModel;
            _actionSubmitter = actionSubmitter;
            _casterId = casterId;
            _targetId = targetId;
        }

        public IReactiveProperty<string> ActionName => _abilityModel.Name;

        public void SubmitAction()
        {
            _actionSubmitter.SubmitAction(new ActionInfo
            {
                ActionId = ActionName.Value,
                CasterId = _casterId,
                TargetId = _targetId
            });
        }
    }
}