using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShootEmUp.Model
{
    public class PoolObject<T> where T : Transformable
    {
        private readonly Dictionary<T, GameObject> _poolGameObject = new();

        public event Action<T,Action<T,GameObject>> onInstantiat;

        private T[] _types => _poolGameObject.Keys.ToArray();
        private GameObject[] _gameObjects => _poolGameObject.Values.ToArray();

        public (T, GameObject) Enable(Func<T> creatEntity,Vector2 position, Quaternion rotation)
        {
            for (int i = 0; i < _poolGameObject.Count; i++)
            {
                GameObject gameObject = _gameObjects[i];

                if (gameObject.activeSelf == false)
                    return GetPairFromPoolObject(i, position, rotation);
            }

            onInstantiat.Invoke(creatEntity.Invoke(), AddObject);

            int lastElement = _poolGameObject.Count - 1;
            return GetPairFromPoolObject(lastElement, position, rotation);
        }

        public void Disable(T model)
        {
            for (int i = 0; i < _poolGameObject.Count;i++)
            {
                T type = _types[i];

                if (type.Equals(model))
                {
                    _poolGameObject[type].SetActive(false);
                    return;
                }
            }

            throw new InvalidOperationException();
        }
        
        private void AddObject(T model, GameObject gameObject)
        {
            gameObject.SetActive(false);
             _poolGameObject.Add(model, gameObject);
        }

        private (T,GameObject) GetPairFromPoolObject(int index,Vector2 position,Quaternion rotation)
        {
            GameObject gameObject = _gameObjects[index];
            T model = _types[index];

            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            gameObject.SetActive(true);

            return (model, gameObject);
        }
    }
}
