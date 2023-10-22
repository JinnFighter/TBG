namespace Visuals.UiService
{
    public interface IViewPool
    {
        void Init();
        void Terminate();
        T TakeItem<T>() where T : BaseView;
        void Release<T>(T item) where T : BaseView;
    }
}