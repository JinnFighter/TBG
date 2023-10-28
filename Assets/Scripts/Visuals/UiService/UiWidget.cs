using Reactivity;

namespace Visuals.UiService
{
    public abstract class UiWidget<TModel, TView> : IUiWidget where TModel : IModel where TView : UiView
    {
        protected readonly SubscriptionAggregator SubscriptionAggregator = new();
        private IUiService _uiService;
        protected TModel Model { get; private set; }
        protected TView View { get; private set; }

        public void Init()
        {
            RegisterChildWidgets();
            InitInner();
        }

        public void Terminate()
        {
            SubscriptionAggregator.Unsubscribe();

            TerminateInner();
        }

        public void Setup(IModel model, UiView view, IUiService uiService)
        {
            Model = (TModel)model;
            View = (TView)view;
            _uiService = uiService;
        }

        protected virtual void InitInner()
        {
        }

        protected virtual void TerminateInner()
        {
        }


        protected virtual void RegisterChildWidgets()
        {
        }

        protected void RegisterChildWidget<TWidget>(IModel model, UiView view)
            where TWidget : IUiEmbeddedWidget
        {
            _uiService.OpenEmbedded<TWidget>(model, view, View.transform);
        }
    }

    public abstract class UiScreen<TModel, TView> : UiWidget<TModel, TView>, IUiScreen
        where TModel : IModel where TView : UiView
    {
    }

    public abstract class UiDialog<TModel, TView> : UiWidget<TModel, TView>, IUiDialog
        where TModel : IModel where TView : UiView
    {
    }

    public abstract class UiEmbeddedWidget<TModel, TView> : UiWidget<TModel, TView>, IUiEmbeddedWidget
        where TModel : IModel where TView : UiView
    {
    }

    public interface IUiScreen : IUiWidget
    {
    }

    public interface IUiDialog : IUiWidget
    {
    }

    public interface IUiEmbeddedWidget : IUiWidget
    {
    }
}