using UnityEngine;
using Localization;
using TMPro;
using UserInterface;
using UnityEngine.UI;
using System.Collections.Generic;


namespace Lessons
{
    public sealed class LessonEditorView : MonoBehaviour,

        IEditor
    {

        public Languages CurrentLanguage
        {

            get => (Languages)_dropdown.value;
        }


        public string NameOfLesson
        {

            get => _nameOfLesson.text;
        }


        #region Clicked events

        public Button.ButtonClickedEvent CreateClicked
        {

            get => _createButton.onClick;
        }


        public Button.ButtonClickedEvent CloseClicked
        {

            get => _closeButton.onClick;
        }


        public Button.ButtonClickedEvent LoadIconClicked
        {

            get => _loadIconButton.onClick;
        }

        #endregion


        public TMP_Dropdown.DropdownEvent LanguageChanged
        {

            get => _dropdown.onValueChanged;
        }


        #region Buttons

        [SerializeField, Space]

        private Button _createButton;


        [SerializeField, Space]

        private Button _closeButton;


        [SerializeField, Space]

        private Button _loadIconButton;

        #endregion


        #region Input Field Handlers

        [SerializeField, Space]
        
        private TMP_InputField _nameOfLesson;


        [SerializeField, Space]
        
        private TMP_InputField _introductionText;


        [SerializeField, Space] 
        
        private TMP_InputField _usageText;


        [SerializeField, Space ] 
        
        private TMP_InputField _descriptionText;

        #endregion


        [SerializeField, Space]

        private TextMeshProUGUI _iconPathHandler;


        [SerializeField, Space]

        private TMP_Dropdown _dropdown;


        public LessonTextContent GetTextContent()
        {
            
            return new LessonTextContent(_introductionText.text,

                _usageText.text, _descriptionText.text);
        }


        public void SetTextContent(LessonTextContent content)
        {

            _introductionText.text = content.Introduction;

            _usageText.text = content.Usage;

            _descriptionText.text = content.PracticeDescription;
        }


        public void SetLanguages(IEnumerable<Languages> languages)
        {

            _dropdown.ClearOptions();


            foreach(Languages language in languages)
            {

                TMP_Dropdown.OptionData option = 
                    
                    new(language.ToString());


                _dropdown.options.Add(option);
            }


            _dropdown.RefreshShownValue();
        }


        public void SetIconPath(string iconPath)
        {

            _iconPathHandler.text = iconPath;
        }
    }
}