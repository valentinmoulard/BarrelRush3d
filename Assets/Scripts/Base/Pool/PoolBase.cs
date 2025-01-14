using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Base.Pool
{
    public abstract class PoolBase<T> : MonoBehaviour where T : Object
    {
        [SerializeField]
        private T[] prefabs;
        private readonly Dictionary<int, ObjectPool<T>> _pools = new();

        public void InitializePools()
        {
            _pools.Clear();
            for (int i = 0; i < prefabs.Length; i++)
            {
                int tempIndex = i;
                var pool = new ObjectPool<T>(() => OnCreate(tempIndex), OnGet, OnReturn);
                _pools.Add(tempIndex, pool);
            }
        }

        private T OnCreate(int index)
        {
            if (index < 0 || index >= prefabs.Length)
            {
                Debug.LogError($"Attempted to access prefab at index {index}, but prefabs length is {prefabs.Length}. Returning null.");
                return null;
            }
            return Instantiate(prefabs[index], transform);
        }

        public T GetObject(int index = 0)
        {
            return _pools[index].Get();
        }

        public void ReturnObject(T item, int poolIndex = 0)
        {
            _pools[poolIndex].Release(item);
        }

        protected virtual void OnGet(T item)
        {
            if (item is GameObject gO)
            {
                gO.SetActive(true);
            }
            else if (item is Component component)
            {
                component.gameObject.SetActive(true);
            }
        }

        private void OnReturn(T item)
        {
            if (item is GameObject gO)
            {
                gO.SetActive(false);
            }
            else if (item is Component component)
            {
                component.gameObject.SetActive(false);
            }
        }
    }
}