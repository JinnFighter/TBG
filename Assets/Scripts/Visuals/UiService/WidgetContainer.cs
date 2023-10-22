using System;
using System.Collections.Generic;
using UnityEngine;
using Visuals.Ui.Hud;

namespace Visuals.UiService
{
    [CreateAssetMenu(menuName = "Create WidgetContainer", fileName = "WidgetContainer", order = 0)]
    public class WidgetContainer : BaseViewContainer
    {
        [SerializeField] private HudView _hudView;

        public override void Init()
        {
            Views = new Dictionary<Type, BaseView>
            {
                {typeof(HudView), _hudView}
            };
        }
    }
}