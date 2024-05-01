using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace UserInterface.Buttons
{
    public class ButtonElement
    {
        public event UnityAction Clicked;
        public RectTransform rectTransform { get; private set; }
        protected Image Image { get; private set; }
        protected TextMeshProUGUI Text { get; private set; }

        protected GameObject _obj;
        private GameObject _child;
        private Button _button;

        private Vector2 _size;

        public ButtonElement(string name, Vector2 size)
        {
            _size = size;

            _obj = new GameObject(name);
            _child = new GameObject(name + " Text");

            _button = _obj.AddComponent<Button>();
            _button.onClick.AddListener(() => Clicked?.Invoke());

            SetSizes();
            SetImage();
            SetText();
            SetParent();
        }

        private void SetSizes()
        {
            rectTransform = _obj.AddComponent<RectTransform>();
            rectTransform.sizeDelta = _size;
            rectTransform.anchorMin = new Vector2(0f, 0.5f);
            rectTransform.anchorMax = new Vector2(0f, 0.5f);
        }
        private void SetImage()
        {
            Image = _obj.AddComponent<Image>();
            Image.preserveAspect = true;

            _button.targetGraphic = Image;
        }
        private void SetText()
        {
            Text = _child.AddComponent<TextMeshProUGUI>();
            Text.rectTransform.sizeDelta = _size;
            Text.alignment = TextAlignmentOptions.Center;
            Text.color = Color.black;
            Text.fontSize = 36;
        }
        private void SetParent()
        {
            var rect = _child.GetComponent<RectTransform>();
            rect.localPosition = Vector3.zero;
            rect.SetParent(rectTransform, true);
        }
    }
}