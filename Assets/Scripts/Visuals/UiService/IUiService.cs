namespace Visuals.UiService
{
    public interface IUiService
    {
        void Init();
        void Terminate();

        void OpenScreen<TModel, TView>(TModel model, string widgetName, OpenParams openParams = null)
            where TModel : IModel where TView : BaseView;

        void CloseScreen<TModel, TView>(IModel model, OpenParams openParams = null)
            where TModel : IModel where TView : BaseView;
    }
}