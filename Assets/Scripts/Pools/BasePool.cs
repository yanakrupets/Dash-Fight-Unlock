using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Pools
{
    public abstract class BasePool<T> : MonoBehaviour, IPool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> _pool = new();
    
        public virtual T Get()
        {
            var item = _pool.Count == 0
                ? Create()
                : _pool.Dequeue();
            
            item.gameObject.SetActive(true);
            return item;
        }

        public virtual void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
            obj.transform.SetParent(transform);
        }

        protected abstract T Create();
    }
}
