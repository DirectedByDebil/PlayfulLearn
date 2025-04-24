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


        #region Get Label

        public static Label GetLabel(this VisualElement element, string name)
        {

            return element.Q<Label>(name);
        }


        public static Label GetLabel(this UIDocument doc, string name)
        {

            return doc.rootVisualElement.GetLabel(name);
        }

        #endregion


        #region Get Text Field

        public static TextField GetTextField(this UIDocument doc, string name)
        {

            return doc.rootVisualElement.GetTextField(name);
        }


        public static TextField GetTextField(this VisualElement element, string name)
        {

            return element.Q<TextField>(name);
        }

        #endregion


        public static ProgressBar GetProgressBar(this VisualElement element, string name)
        {

            return element.Q<ProgressBar>(name);
        }


        #region Swap Style

        public static void SwapStyle(this VisualElement element, string remove, string add)
        {

            element.RemoveFromClassList(remove);

            element.AddToClassList(add);
        }


        public static void SwapIf(this VisualElement element, bool condition,
            string negative, string positive)
        {

            if(condition)
            {

                element.SwapStyle(negative, positive);
            }
            else
            {

                element.SwapStyle(positive, negative);
            }
        }

        #endregion
    }
}
