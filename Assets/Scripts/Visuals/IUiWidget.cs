using Visuals.UiService;

namespace Visuals
{
    public interface IUiWidget
    {
        void Init();
        void Terminate();
        void Setup(IModel model, UiView view, IUiService uiService);
    }
}