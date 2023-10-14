using System.Collections.Generic;

namespace Visuals
{
    public abstract class BaseController<TModel, TView> : IController where TModel : IModel where TView : IView
    {
        private readonly List<IController> _childControllers = new();
        protected TModel Model;
        protected TView View;

        protected BaseController(TModel model, TView view)
        {
            Model = model;
            View = view;
        }

        public void Init()
        {
            RegisterChildControllers();
            InitInner();

            foreach (var controller in _childControllers) controller.Init();
        }

        public void Terminate()
        {
            foreach (var controller in _childControllers) controller.Terminate();

            _childControllers.Clear();

            TerminateInner();
        }

        protected virtual void InitInner()
        {
        }

        protected virtual void TerminateInner()
        {
        }


        protected virtual void RegisterChildControllers()
        {
        }

        protected void RegisterChildController(IController controller)
        {
            _childControllers.Add(controller);
        }
    }
}