using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Presentation
{
    public class ButtonElement
    {
        public event UnityEngine.Events.UnityAction onClickEvent;
        public RectTransform rectTransform { get; private set; }
        protected Image Image { get; private set; }
        protected TextMeshProUGUI Text { get; private set; }

        private GameObject _obj, _child;
        private Button _button;

        private Vector2 _size;

        public ButtonElement(string name, Vector2 size)
        {
            _size = size;

            _obj = new GameObject(name);
            _child = new GameObject(name + " Text");

            _button = _obj.AddComponent<Button>();
            _button.onClick.AddListener(() => onClickEvent?.Invoke());

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
        private void SetParent()
        {
            var rect = _child.GetComponent<RectTransform>();
            rect.localPosition = Vector3.zero;
            rect.SetParent(rectTransform, true);
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
    }
}