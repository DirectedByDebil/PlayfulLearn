using UnityEngine;
using System.Collections.Generic;


namespace UserInterface
{
    public sealed class TogglesGrid : MonoBehaviour
    {

        public IReadOnlyList<ExpandedToggle> Toggles
        {

            get => _toggles;
        }


        [SerializeField, Space, Range(0, 400)]

        private float _dy;


        [SerializeField, Space]

        private List<ExpandedToggle> _toggles;


        private void OnValidate()
        {

            ReplaceToggles();
        }


        public void SetToggleCount(int count)
        {

            if(_toggles.Count < count)
            {

                InstantialeToggles(count - _toggles.Count);

                ReplaceToggles();
            }
        }


        private void ReplaceToggles()
        {

            Vector3 originalPos = _toggles[0].transform.position;


            for(int i = 1; i < _toggles.Count; i++)
            {

                originalPos.y -= _dy;

                _toggles[i].transform.position = originalPos;
            }
        }


        private void InstantialeToggles(int amount)
        {

            ExpandedToggle original = _toggles[0];


            for(int i = 0; i < amount; i++)
            {

                ExpandedToggle toggle = Instantiate(original, transform, false);
                
                _toggles.Add(toggle);
            }
        }
    }
}