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


        public IReadOnlyList<ExpandedToggle> Toggles
        {

            get => _toggleGrid.Toggles;
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


        [Space, SerializeField]

        private TogglesGrid _toggleGrid;


        #region Buttons

        [SerializeField, Space]

        private Button _createButton;


        [SerializeField, Space]

        private Button _closeButton;

        #endregion



        public void SetAllLessons(IReadOnlyList<Lesson> lessons)
        {

            _toggleGrid.SetToggleCount(lessons.Count);


            for(int i = 0; i < lessons.Count; i++)
            {

                Toggles[i].UpdateText(lessons[i].NameOfLesson);
            }
        }
    }
}