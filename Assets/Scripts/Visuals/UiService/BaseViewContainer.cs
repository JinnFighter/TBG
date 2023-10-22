using System;
using System.Collections.Generic;
using UnityEngine;

namespace Visuals.UiService
{
    public abstract class BaseViewContainer : ScriptableObject, IViewContainer
    {
        protected Dictionary<Type, BaseView> Views;

        public abstract void Init();

        public T GetView<T>() where T : BaseView
        {
            return Views[typeof(T)] as T;
        }
    }
}