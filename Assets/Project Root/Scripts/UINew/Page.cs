using UnityEngine;
using UnityEngine.UIElements;

namespace UINew
{

    [RequireComponent(typeof(UIDocument))]
    public class Page : MonoBehaviour, IPage
    {

        protected UIDocument document;


        public virtual void Init()
        {

            document = GetComponent<UIDocument>();
        }


        public void Show()
        {

            document.rootVisualElement.style.display = DisplayStyle.Flex;
        }


        public void Hide()
        {

            document.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
}
