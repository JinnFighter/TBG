using System;
using UnityEngine;

namespace Visuals.UiService
{
    public interface IUiService
    {
        void Init();
        void Terminate();

        WidgetReference Open<TWidget>(IModel model, Type viewType) where TWidget : IUiWidget;

        WidgetReference OpenEmbedded<TWidget>(IModel model, UiView view, Transform parent)
            where TWidget : IUiEmbeddedWidget;

        void Close<TWidget>(IModel model) where TWidget : IUiWidget;
    }
}