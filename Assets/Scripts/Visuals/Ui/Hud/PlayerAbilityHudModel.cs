using Reactivity;
using Visuals.Characters;

namespace Visuals.Ui.Hud
{
    public class PlayerAbilityHudModel : IPlayerAbilityHudModel
    {
        private readonly ICharacterAbilityModel _abilityModel;
        private readonly int _casterId;
        private readonly int _targetId;

        public PlayerAbilityHudModel(ICharacterAbilityModel characterAbilityModel, int casterId, int targetId)
        {
            _abilityModel = characterAbilityModel;
            _casterId = casterId;
            _targetId = targetId;
        }

        public IReactiveProperty<string> ActionName => _abilityModel.Name;

        public void SubmitAction()
        {
            _abilityModel.SubmitAction(_casterId, _targetId);
        }
    }
}