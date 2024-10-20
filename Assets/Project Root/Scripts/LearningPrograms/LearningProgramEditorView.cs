using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Lessons;
using TMPro;
using UserInterface;


namespace LearningPrograms
{
    public class LearningProgramEditorView : UIObject,
        
        IEditor
    {

        public string NameOfProgram
        {

            get => _nameOfProgramText.text;
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


        [Space, SerializeField]
        
        private TMP_InputField _nameOfProgramText;


        [Space, SerializeField]

        private TextMeshProUGUI _iconPathHandler;


        #region Buttons

        [SerializeField, Space]

        private Button _createButton;


        [SerializeField, Space]

        private Button _closeButton;


        [SerializeField, Space]

        private Button _loadIconButton;

        #endregion



        public void SetAllLessons(IReadOnlyCollection<Lesson> lessons,
            
            IReadOnlyList<ExpandedToggle> toggles)
        {

            int index = 0;


            foreach(Lesson lesson in lessons)
            {

                toggles[index].UpdateText(lesson.NameOfLesson);

                index++;
            }
        }


        public void SetIconPath(string iconPath)
        {

            _iconPathHandler.text = iconPath;
        }
    }
}