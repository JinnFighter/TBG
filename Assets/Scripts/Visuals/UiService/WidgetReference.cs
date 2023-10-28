namespace Visuals.UiService
{
    public class WidgetReference
    {
        private readonly UiView _view;
        private readonly ViewPool _viewPool;
        private readonly IUiWidget _widget;

        public WidgetReference(ViewPool viewPool, IUiWidget widget, UiView view)
        {
            _viewPool = viewPool;
            _widget = widget;
            _view = view;
        }

        public void Open()
        {
            _widget.Init();
        }

        public void Close()
        {
            _widget.Terminate();
            _viewPool.Release(_view);
        }
    }
}