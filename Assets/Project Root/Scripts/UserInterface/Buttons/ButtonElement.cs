using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace UserInterface.Buttons
{
    public class ButtonElement
    {
        public ButtonElement(string name, ButtonElementSettings settings)
        {
            _settings = settings;

            parentObject = new GameObject(name);
            _iconHandler = new GameObject(name + " Icon");
            _textHandler = new GameObject(name + " Text");

            _button = parentObject.AddComponent<Button>();
            _button.onClick.AddListener(() => Clicked?.Invoke());

            SetBackground();
            SetSizes();

            SetIcon();
            SetIconParent();

            SetText();
            SetTextParent();
        }

        public event UnityAction Clicked;
        public RectTransform rectTransform { get; private set; }
        protected Image Icon { get; private set; }
        protected TextMeshProUGUI Text { get; private set; }

        protected GameObject parentObject;

        private GameObject _textHandler, _iconHandler;
        private Button _button;
        private ButtonElementSettings _settings;

        private void SetBackground()
        {
            Image background = parentObject.AddComponent<Image>();
            background.preserveAspect = true;
            background.sprite = _settings.Background;
        }
        private void SetSizes()
        {
            rectTransform = (RectTransform)parentObject.transform;
            rectTransform.sizeDelta = _settings.Size;
            rectTransform.anchorMin = new Vector2(0f, 0.5f);
            rectTransform.anchorMax = new Vector2(0f, 0.5f);
        }

        private void SetIcon()
        {
            Icon = _iconHandler.AddComponent<Image>();
            Icon.preserveAspect = true;
            Icon.color = _settings.ColorBlock.normalColor;

            _button.targetGraphic = Icon;
            _button.colors = _settings.ColorBlock;
        }
        private void SetIconParent()
        {
            RectTransform rect = (RectTransform)_iconHandler.transform;
            rect.sizeDelta = _settings.IconSize;
            rect.SetParent(rectTransform, true);
            rect.localPosition = _settings.IconLocalPosition;
        }

        private void SetText()
        {
            Text = _textHandler.AddComponent<TextMeshProUGUI>();
            Text.rectTransform.sizeDelta = new Vector2(_settings.Size.x, _settings.Size.y - 2*_settings.TextLocalPosition.y);
            
            Text.font = _settings.Font;
            Text.fontSize = _settings.FontSize;
            Text.fontStyle = _settings.FontStyle;
            Text.alignment = _settings.TextAlignmentOptions;
            
            Text.color = Color.black;
        }
        private void SetTextParent()
        {
            RectTransform rect = (RectTransform)_textHandler.transform;
            rect.localPosition = _settings.TextLocalPosition;
            rect.SetParent(rectTransform, true);
        }
    }
}