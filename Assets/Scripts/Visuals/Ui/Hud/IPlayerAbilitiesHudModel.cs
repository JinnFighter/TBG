using System.Collections.Generic;

namespace Visuals.Ui.Hud
{
    public interface IPlayerAbilitiesHudModel : IModel
    {
        List<IPlayerAbilityHudModel> PlayerActions { get; }
    }
}