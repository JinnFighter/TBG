using System.Collections.Generic;

namespace Visuals.Ui.Hud
{
    public interface IPlayerActionsHudModel : IModel
    {
        List<IPlayerActionHudModel> PlayerActions { get; }
    }
}