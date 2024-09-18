using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UserInterface
{
    public sealed class ExpandedToggle : Toggle
    {

        [SerializeField]

        private TextMeshProUGUI _textHandler;


        public void UpdateText(string text)
        {

            _textHandler.text = text;
        }
    }
}