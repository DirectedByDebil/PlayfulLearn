using System.Collections.Generic;


namespace UserInterface
{
    public sealed class UIModel
    {

        private Dictionary<UIStates, List<UIObject>> _all;


        private List<UIObject> _currentActive;



        public UIModel(UIStates[] states)
        {

            InitializeDictionary(states);


            _currentActive = new List<UIObject>();
        }


        public void LoadObjects(IReadOnlyList<UIObject> objects)
        {
        
            foreach(UIObject uiObject in objects)
            {

                LoadObject(uiObject);
            }
        }


        public void ChangeState(UIStates state)
        {

            SetActiveCurrentObjects(false);


            _currentActive = _all[state];


            SetActiveCurrentObjects(true);
        }



        private void InitializeDictionary(UIStates[] states)
        {

            _all = new Dictionary<UIStates,
                
                List<UIObject>>(states.Length);


            foreach(UIStates state in states)
            {

                _all.Add(state, new List<UIObject>());
            }
        }


        private void LoadObject(UIObject uiObject)
        {

            if(uiObject.HasManyStates)
            {

                foreach(UIStates state in uiObject.States)
                {

                    AddUIObject(state, uiObject);
                }
            }
            else
            {

                AddUIObject(uiObject.State, uiObject);
            }
        }


        private void AddUIObject(UIStates state, UIObject uiObject)
        {

            _all[state].Add(uiObject);
        }


        private void SetActiveCurrentObjects(bool isActive)
        {

            foreach (UIObject uiObject in _currentActive)
            {

                uiObject.gameObject.SetActive(isActive);
            }
        }
    }
}
