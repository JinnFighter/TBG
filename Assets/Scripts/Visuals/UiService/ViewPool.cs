using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Visuals.UiService
{
    public class ViewPool : MonoBehaviour, IViewPool
    {
        [SerializeField] private Transform _poolStash;
        [SerializeField] private BaseViewContainer _viewContainer;
        private readonly Dictionary<Type, Queue<BaseView>> _pooledObjects = new();

        public void Init()
        {
            _viewContainer.Init();
        }

        public void Terminate()
        {
            foreach (var view in _pooledObjects.Keys.SelectMany(type => _pooledObjects[type]))
                Destroy(view.gameObject);

            _pooledObjects.Clear();
        }

        public T TakeItem<T>() where T : BaseView
        {
            T result;
            var type = typeof(T);
            if (_pooledObjects.ContainsKey(type) && _pooledObjects[type].Count > 0)
            {
                result = (T) _pooledObjects[type].Dequeue();
                result.transform.SetParent(null);
                result.enabled = true;
            }

            else
            {
                result = Instantiate(_viewContainer.GetView<T>());
            }

            return result;
        }

        public BaseView TakeItem(Type type)
        {
            BaseView result;
            if (_pooledObjects.ContainsKey(type) && _pooledObjects[type].Count > 0)
            {
                result = _pooledObjects[type].Dequeue();
                result.transform.SetParent(null);
                result.enabled = true;
            }

            else
            {
                result = Instantiate(_viewContainer.GetView(type));
            }

            return result;
        }

        public void Release<T>(T item) where T : BaseView
        {
            var type = typeof(T);
            if (!_pooledObjects.ContainsKey(type)) _pooledObjects[type] = new Queue<BaseView>();

            _pooledObjects[type].Enqueue(item);
            item.enabled = false;
            item.transform.SetParent(_poolStash, false);
        }
    }
}