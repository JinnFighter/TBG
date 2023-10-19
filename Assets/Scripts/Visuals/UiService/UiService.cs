using System;
using System.Collections.Generic;
using UnityEngine;

namespace Visuals.UiService
{
    public class UiService : MonoBehaviour, IUiService
    {
        [SerializeField] private Canvas _layerScreen;
        [SerializeField] private WidgetContainer _widgetPrefabContainer;

        private readonly Dictionary<EUiLayer, LayerWidgetsContainer> _widgets = new()
        {
            {EUiLayer.Screen, new LayerWidgetsContainer()}
        };

        public void Init()
        {
            Debug.Log("Init Ui Service");
        }

        public void Terminate()
        {
            Debug.Log("Destroy Ui Service");
            foreach (var value in Enum.GetValues(typeof(EUiLayer)))
            {
                var layer = (EUiLayer) value;
                var keys = new List<IModel>(_widgets[layer].Widgets.Keys);
                foreach (var key in keys) CloseWidget(key, layer);
            }

            _widgets.Clear();
        }

        public TView OpenScreen<TModel, TView>(TModel model, string widgetName, OpenParams openParams = null)
            where TModel : IModel where TView : BaseView
        {
            var container = _widgets[EUiLayer.Screen];
            if (!container.Widgets.ContainsKey(model))
            {
                var widget = Instantiate<BaseView>(_widgetPrefabContainer.GetWidget<TView>(widgetName),
                    _layerScreen.transform, true);
                container.Widgets[model] = widget;

                return widget as TView;
            }

            return null;
        }

        public void CloseScreen<TModel, TView>(IModel model, OpenParams openParams = null)
            where TModel : IModel where TView : BaseView
        {
            CloseWidget(model, EUiLayer.Screen);
        }

        private void CloseWidget(IModel model, EUiLayer layer)
        {
            var container = _widgets[layer];
            if (_widgets[layer].Widgets.TryGetValue(model, out var widget))
            {
                widget.transform.SetParent(null);
                container.Widgets.Remove(model);
                Destroy(widget);
            }
        }
    }

    internal class LayerWidgetsContainer
    {
        public Dictionary<IModel, BaseView> Widgets { get; } = new();
    }
}