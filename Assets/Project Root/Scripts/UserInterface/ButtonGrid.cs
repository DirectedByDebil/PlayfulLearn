using UnityEngine;
using System.Collections.Generic;

namespace UserInterface
{
    public sealed class ButtonGrid : MonoBehaviour
    {

        [SerializeField, Range(1, 5)]

        private int _objectsInRow;


        [SerializeField, Space]

        private Vector2 _padding;


        private Vector2 _startPosition;


        private List<ExpandedButton> _buttons = new();


        private void OnValidate()
        {

            if(_buttons.Count == 0)
            {

                UpdateButtons();
            }

            
            _startPosition = _buttons[0].transform.position;

            ReplaceButtons();
        }


        public void UpdateButtons()
        {

            _buttons.Clear();


            _buttons = new List<ExpandedButton>(transform.childCount);


            foreach(Transform child in transform)
            {

                _buttons.Add(
                        
                 child.gameObject.GetComponent<ExpandedButton>());
            }
        }

        
        private void ReplaceButtons()
        {

            for(int i = 1; i < _buttons.Count; i++)
            {

                _buttons[i].transform.position =

                    CountPosition(i);
            }
        }


        private Vector3 CountPosition(int index)
        {

            Vector2 newPosition = new()
            {

                x = index % _objectsInRow,

                y = - index / _objectsInRow
            };


            newPosition *= _padding;

            newPosition += _startPosition;


            return newPosition;
        }
    }
}