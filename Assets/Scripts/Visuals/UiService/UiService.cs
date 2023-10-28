using System;
using System.Collections.Generic;
using UnityEngine;

namespace Visuals.UiService
{
    public class UiService : MonoBehaviour, IUiService
    {
        [SerializeField] private Canvas _layerScreen;
        [SerializeField] private Canvas _layerDialog;

        [SerializeField] private ViewPool _viewPool;
        private readonly Dictionary<IModel, WidgetReference> _widgets = new();

        public void Init()
        {
            Debug.Log("Init Ui Service");
            _viewPool.Init();
        }

        public void Terminate()
        {
            Debug.Log("Destroy Ui Service");

            foreach (var kvp in _widgets)
            {
                kvp.Value.Close();
            }

            _widgets.Clear();
            _viewPool.Terminate();
        }

        public WidgetReference Open<TWidget>(IModel model, Type viewType) where TWidget : IUiWidget
        {
            if (!_widgets.ContainsKey(model))
            {
                var widget = Activator.CreateInstance<TWidget>();
                var layer = widget switch
                {
                    IUiScreen => _layerScreen.transform,
                    IUiDialog => _layerDialog.transform,
                    _ => throw new Exception("Tried to Create embedded widget as not embedded")
                };
                var view = _viewPool.TakeItem(viewType) as UiView;
                view.transform.SetParent(layer, false);
                widget.Setup(model, view, this);

                var widgetReference = new WidgetReference(_viewPool, widget, view);
                _widgets[model] = widgetReference;

                widgetReference.Open();

                return widgetReference;
            }

            return null;
        }

        public WidgetReference OpenEmbedded<TWidget>(IModel model, UiView view, Transform parent)
            where TWidget : IUiEmbeddedWidget
        {
            if (!_widgets.ContainsKey(model))
            {
                var widget = Activator.CreateInstance<TWidget>();
                widget.Setup(model, view, this);

                view.transform.SetParent(parent, false);
                var widgetReference = new WidgetReference(_viewPool, widget, view);
                _widgets[model] = widgetReference;

                widgetReference.Open();

                return widgetReference;
            }

            return null;
        }

        public void Close<TWidget>(IModel model) where TWidget : IUiWidget
        {
            if (_widgets.TryGetValue(model, out var widgetReference))
            {
                widgetReference.Close();
                _widgets.Remove(model);
            }
        }
    }
}