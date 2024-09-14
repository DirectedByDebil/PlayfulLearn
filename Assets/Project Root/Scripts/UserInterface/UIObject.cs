using UnityEngine;
using System.Collections.Generic;


namespace UserInterface
{
    public class UIObject : MonoBehaviour
    {

        public bool HasManyStates
        {

            get => _hasManyStates;
        }
        

        public UIStates State
        {

            get => _state;
        }


        public IReadOnlyList<UIStates> States
        {

            get => _states;
        }



        [SerializeField, Space]

        protected bool _hasManyStates;


        [SerializeField, Space, HideInInspector]

        protected UIStates _state;


        [SerializeField, Space, HideInInspector]

        protected List<UIStates> _states;
    }
}
