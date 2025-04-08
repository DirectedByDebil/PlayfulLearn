using UnityEngine.UIElements;

namespace UINew
{
    public static class UIExtensions
    {

        #region Get List/Scrool View

        public static ListView GetListView(this UIDocument doc, string name)
        {

            return doc.rootVisualElement.Q<ListView>(name);
        }


        public static ScrollView GetScrollView(this UIDocument doc, string name)
        {

            return doc.rootVisualElement.Q<ScrollView>(name);
        }

        #endregion


        #region Get Button

        public static Button GetButton(this VisualElement element, string name)
        {

            return element.Q<Button>(name);
        }


        public static Button GetButton(this UIDocument doc, string name)
        {

            return doc.rootVisualElement.GetButton(name);
        }

        #endregion


        #region Get Element

        public static VisualElement GetElement(this VisualElement element, string name)
        {

            return element.Q<VisualElement>(name);
        }


        public static VisualElement GetElement(this UIDocument doc, string name)
        {

            return doc.rootVisualElement.GetElement(name);
        }
        
        #endregion


        public static Label GetLabel(this VisualElement element, string name)
        {

            return element.Q<Label>(name);
        }
    }
}
