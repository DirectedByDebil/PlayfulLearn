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

        #endregion


        [Space, SerializeField]
        
        private TMP_InputField _nameOfProgramText;


        #region Buttons

        [SerializeField, Space]

        private Button _createButton;


        [SerializeField, Space]

        private Button _closeButton;

        #endregion



        public void SetAllLessons(IReadOnlyCollection<NewLesson> lessons,
            
            IReadOnlyList<ExpandedToggle> toggles)
        {

            int index = 0;


            foreach(NewLesson lesson in lessons)
            {

                toggles[index].UpdateText(lesson.NameOfLesson);

                index++;
            }
        }
    }
}