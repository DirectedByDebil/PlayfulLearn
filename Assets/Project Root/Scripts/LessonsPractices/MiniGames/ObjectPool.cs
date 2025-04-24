using UnityEngine;
using System.Collections.Generic;

namespace LessonsPractices.MiniGames
{
    public sealed class ObjectPool<T> where T : MonoBehaviour
    {

        public List<T> Objects
        {
            get => _objects;
        }


        private readonly Transform _root;

        private readonly List<T> _objects;


        public ObjectPool(Transform root)
        {

            _root = root;

            _objects = new List<T>();
        }



        public void UpdateObjects()
        {

            _objects.Clear();


            foreach (Transform child in _root)
            {

                if(child.TryGetComponent(out T obj))
                {

                    _objects.Add(obj);
                }
            }
        }


        public bool TryGetObject(out T obj)
        {

            obj = _objects.Find(x => !x.gameObject.activeInHierarchy);

            return obj;
        }
    }
}
