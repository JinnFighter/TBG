using System;
using System.Collections.Generic;
using UnityEngine;
using Visuals.Ui.GameOver;
using Visuals.Ui.Hud;

namespace Visuals.UiService
{
    [CreateAssetMenu(menuName = "Create WidgetContainer", fileName = "WidgetContainer", order = 0)]
    public class WidgetContainer : BaseViewContainer
    {
        [SerializeField] private HudView _hudView;
        [SerializeField] private GameOverDialogView _gameOverDialogView;

        public override void Init()
        {
            Views = new Dictionary<Type, BaseView>
            {
                {typeof(HudView), _hudView},
                {typeof(GameOverDialogView), _gameOverDialogView}
            };
        }
    }
}