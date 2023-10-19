using UnityEngine;

namespace Visuals.UiService
{
    [CreateAssetMenu(menuName = "Create WidgetContainer", fileName = "WidgetContainer", order = 0)]
    public class WidgetContainer : ScriptableObject
    {
        public T GetWidget<T>(string widgetName) where T : BaseView
        {
            return widgetName switch
            {
                _ => null
            };
        }
    }
}