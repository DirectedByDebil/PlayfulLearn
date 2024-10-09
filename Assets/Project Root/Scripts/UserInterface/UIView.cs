using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UserInterface
{
    public sealed class UIView : MonoBehaviour
    {

        public IReadOnlyList<UIObject> UIObjects
        {

            get => _uiObjects;
        }    



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


        public Button.ButtonClickedEvent OpenProgramEditorClicked
        {

            get => _openProgramEditorButton.onClick;
        }


        public Button.ButtonClickedEvent UserAccountClicked
        {

            get => _openUserAccountButton.onClick;
        }

        #endregion


        [SerializeField, Space]

        private List<UIObject> _uiObjects;



        #region Buttons

        [SerializeField, Space]

        private Button _availableProgramsButton;


        [SerializeField, Space]

        private Button _backToProgramButton;


        [SerializeField, Space]

        private Button _openLessonEditorButton;


        [SerializeField, Space]

        private Button _openProgramEditorButton;


        [SerializeField, Space]

        private Button _openUserAccountButton;

        #endregion
    }
}