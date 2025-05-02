using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace UINew
{

    [RequireComponent(typeof(UIDocument))]
    public class Page : MonoBehaviour, IPage
    {

        public event Action Closing;


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


        #region Closing

        protected void StartClosing()
        {
            Closing?.Invoke();
        }


        protected void StartClosing(ClickEvent e)
        {

            Closing?.Invoke();
        }
        
        #endregion
    }
}
