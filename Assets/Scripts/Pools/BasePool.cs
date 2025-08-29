using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Pools
{
    public abstract class BasePool<T> : MonoBehaviour, IPool<T> where T : MonoBehaviour
    {
        protected readonly Queue<T> Pool = new();
    
        public virtual T Get()
        {
            var item = Pool.Count == 0
                ? Create()
                : Pool.Dequeue();
            
            item.gameObject.SetActive(true);
            return item;
        }

        public virtual void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            Pool.Enqueue(obj);
            obj.transform.SetParent(transform);
        }

        protected abstract T Create();
    }
}
