using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UserInterface
{
    public sealed class ExpandedButton : Button
    {

        [SerializeField, Space]

        private TextMeshProUGUI _textHandler;


        [SerializeField, Space]

        private Image _icon;


        public void UpdateText(string text)
        {

            _textHandler.text = text;
        }


        public void UpdateIcon(Sprite icon)
        {

            _icon.sprite = icon;
        }
    }
}