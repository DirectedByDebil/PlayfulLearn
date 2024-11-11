using System.Collections.Generic;
using UnityEngine;

namespace UserInterface
{ 
    public sealed class ObjectGrid : MonoBehaviour
    {

        [SerializeField, Range(1, 5)]

        private int _objectsInRow;


        [SerializeField, Space]

        private Vector2 _padding;


        [SerializeField, Space]

        private List<MonoBehaviour> _objects = new();


        private void OnValidate()
        {

            ReplaceObjects();
        }


        public bool TryUpdateCount(int count)
        {

            if (_objects.Count < count)
            {

                InstantiateObjects(count - _objects.Count);

                ReplaceObjects();


                return true;
            }

            return false;
        }


        public void ShowOnly(int count)
        {

            int showOnlyCount = Mathf.Min(count, _objects.Count);

            int hideCount = Mathf.Max(count, _objects.Count);


            for(int i = 0; i < showOnlyCount; i++)
            {

                _objects[i].gameObject.SetActive(true);
            }


            for(int i = showOnlyCount; i < hideCount; i++)
            {

                _objects[i].gameObject.SetActive(false);
            }
        }


        public IReadOnlyList<T> GetObjects<T>() where T : MonoBehaviour
        {

            List<T> components = new(_objects.Count);


            foreach(MonoBehaviour obj in _objects)
            {

                components.Add(obj.GetComponent<T>());
            }


            return components;
        }


        public T GetObject<T>(int index) where T: MonoBehaviour
        {

            MonoBehaviour obj = _objects[index];


            return obj.GetComponent<T>();
        }


        private void ReplaceObjects()
        {

            Vector3 origPosition = _objects[0].transform.position;


            for (int i = 1; i < _objects.Count; i++)
            {

                _objects[i].transform.position =

                    CountPosition(i, origPosition);
            }
        }


        private Vector3 CountPosition(int index, Vector3 origPosition)
        {

            Vector3 newPosition = new()
            {

                x = index % _objectsInRow,

                y = -index / _objectsInRow
            };


            newPosition *= _padding;

            newPosition += origPosition;


            return newPosition;
        }


        private void InstantiateObjects(int amount)
        {

            MonoBehaviour original = _objects[0];


            for (int i = 0; i < amount; i++)
            {

                MonoBehaviour button = Instantiate(original, transform, false);

                button.gameObject.SetActive(false);


                _objects.Add(button);
            }
        }
    }
}