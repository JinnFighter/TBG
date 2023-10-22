using System.Collections.Generic;

namespace Visuals.Ui.Hud
{
    public class PlayerActionsHudModel : IPlayerActionsHudModel
    {
        public PlayerActionsHudModel(List<IPlayerActionHudModel> playerActions)
        {
            PlayerActions = playerActions;
        }

        public List<IPlayerActionHudModel> PlayerActions { get; }
    }
}