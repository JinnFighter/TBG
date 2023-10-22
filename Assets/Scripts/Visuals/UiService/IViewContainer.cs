namespace Visuals.UiService
{
    public interface IViewContainer
    {
        void Init();
        T GetView<T>() where T : BaseView;
    }
}