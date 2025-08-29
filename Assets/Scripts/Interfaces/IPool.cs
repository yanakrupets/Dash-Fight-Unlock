using UnityEngine;

namespace Interfaces
{
    public interface IPool<T> where T : MonoBehaviour
    {
        T Get();
        void Return(T obj);
    }
}