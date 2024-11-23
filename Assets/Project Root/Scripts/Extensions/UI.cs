using UnityEngine.UIElements;

namespace Extensions
{
    public static class UI
    {

        public static T GetElement<T>(UIDocument doc, string name)
            
            where T: VisualElement
        {

            return doc.rootVisualElement.Q<T>(name);
        }


        public static Button GetButton(UIDocument doc, string name)
        {

            return doc.rootVisualElement.Q<Button>(name);
        }


        public static TextField GetTextField(UIDocument doc, string name)
        {

            return doc.rootVisualElement.Q<TextField>(name);
        }


        public static Label GetLabel(UIDocument doc, string name)
        {

            return doc.rootVisualElement.Q<Label>(name);
        }
    }
}
