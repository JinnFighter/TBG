using UnityEngine;
using Visuals.Ui.Hud;

namespace Visuals.UiService
{
    [CreateAssetMenu(menuName = "Create WidgetContainer", fileName = "WidgetContainer", order = 0)]
    public class WidgetContainer : ScriptableObject
    {
        [SerializeField] private HudView _hudView;
        
        public T GetWidget<T>(string widgetName) where T : BaseView
        {
            return widgetName switch
            {
                "hudView" => _hudView as T,
                _ => null
            };
        }
    }
}