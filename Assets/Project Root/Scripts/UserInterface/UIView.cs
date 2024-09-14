using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UserInterface
{
    public sealed class UIView : MonoBehaviour
    {

        #region ReadOnly Lists

        public IReadOnlyList<UIObject> UIObjects
        {

            get => _uiObjects;
        }    


        public IReadOnlyList<ExpandedButton> LessonButtons
        {

            get => _lessonButtons;
        }


        public IReadOnlyList<ExpandedButton> ProgramButtons
        {

            get => _programButtons;
        }

        #endregion


        #region Clicked events

        public Button.ButtonClickedEvent AvailableProgramsClicked
        {

            get => _availableProgramsButton.onClick;
        }


        public Button.ButtonClickedEvent BackToProgramClicked
        {

            get => _backToProgramButton.onClick;
        }


        public Button.ButtonClickedEvent OpenLessonEditorClicked
        {

            get => _openLessonEditorButton.onClick;
        }


        public Button.ButtonClickedEvent CreateLessonClicked
        {

            get => _createLessonButton.onClick;
        }


        public Button.ButtonClickedEvent CloseLessonEditorClicked
        {

            get => _closeLessonEditorButton.onClick;
        }


        public Button.ButtonClickedEvent OpenProgramEditorClicked
        {

            get => _openProgramEditorButton.onClick;
        }


        public Button.ButtonClickedEvent CreateProgramClicked
        {

            get => _createProgramButton.onClick;
        }


        public Button.ButtonClickedEvent CloseProgramEditorClicked
        {

            get => _closeProgramEditorButton.onClick;
        }



        public IReadOnlyList
            
            <Button.ButtonClickedEvent> LessonsClicked
        {

            get => _lessonsClicked;
        }


        public IReadOnlyList

            <Button.ButtonClickedEvent> ProgramsClicked
        {

            get => _programsClicked;
        }

        #endregion


        [SerializeField, Space]

        private List<UIObject> _uiObjects;


        private List<Button.ButtonClickedEvent> _lessonsClicked = new();

        private List<Button.ButtonClickedEvent> _programsClicked = new();


        #region Buttons

        [SerializeField, Space]

        private Button _availableProgramsButton;


        [SerializeField, Space]

        private Button _backToProgramButton;


        #region Lesson Editor

        [SerializeField, Space]

        private Button _openLessonEditorButton;


        [SerializeField, Space]

        private Button _createLessonButton;


        [SerializeField, Space]

        private Button _closeLessonEditorButton;

        #endregion


        #region Program Editor

        [SerializeField, Space]

        private Button _openProgramEditorButton;


        [SerializeField, Space]

        private Button _createProgramButton;


        [SerializeField, Space]

        private Button _closeProgramEditorButton;

        #endregion


        [SerializeField, Space]

        private List<ExpandedButton> _lessonButtons;


        [SerializeField, Space]

        private List<ExpandedButton> _programButtons;

        #endregion


        private void OnValidate()
        {

            RefillClickedList(_lessonButtons, _lessonsClicked);

            RefillClickedList(_programButtons, _programsClicked);
        }


        private void RefillClickedList(
            
            IReadOnlyList<ExpandedButton> buttons,
            
            List<Button.ButtonClickedEvent> clickedList)
        {

            clickedList.Clear();


            foreach (ExpandedButton button in buttons)
            {

                clickedList.Add(button.onClick);
            }
        }
    }
}