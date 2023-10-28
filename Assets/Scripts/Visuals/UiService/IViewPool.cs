using System;

namespace Visuals.UiService
{
    public interface IViewPool
    {
        void Init();
        void Terminate();
        T TakeItem<T>() where T : BaseView;
        BaseView TakeItem(Type type);
        void Release<T>(T item) where T : BaseView;
    }
}