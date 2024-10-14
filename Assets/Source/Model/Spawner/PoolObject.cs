using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShootEmUp.Model
{
    public class PoolObject<T> where T : Transformable
    {
        private readonly Dictionary<T, GameObject> _poolGameObject = new();

        public event Func<T,GameObject> onCreate;

        private T[] _types => _poolGameObject.Keys.ToArray();
        private GameObject[] _gameObjects => _poolGameObject.Values.ToArray();

        private (T, GameObject) _lastPair;

        public void Enable(Func<T> creatEntity,Vector2 position, Quaternion rotation)
        {
            if (IsEnable(position, rotation))
                return;

            T entity = creatEntity.Invoke();
            GameObject model = onCreate.Invoke(entity);

            AddObject(entity, model);

            int lastElement = _poolGameObject.Count - 1;
            ActiveGameObject(lastElement, position, rotation);
        }

        public (T, GameObject) GetLastActivatedObject()
        => _lastPair;

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

        private bool IsEnable(Vector2 position,Quaternion rotation)
        {
            for (int i = 0; i < _poolGameObject.Count; i++)
            {
                GameObject gameObject = _gameObjects[i];

                if (gameObject.activeSelf == false)
                {
                    ActiveGameObject(i, position, rotation);
                    return true;
                }
            }

            return false;
        }

        private void AddObject(T model, GameObject gameObject)
        {
            gameObject.SetActive(false);
             _poolGameObject.Add(model, gameObject);
        }

        private void ActiveGameObject(int index,Vector2 position,Quaternion rotation)
        {
            GameObject gameObject = _gameObjects[index];
            T model = _types[index];

            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            gameObject.SetActive(true);

            _lastPair = (model, gameObject);

            return;
        }
    }
}
