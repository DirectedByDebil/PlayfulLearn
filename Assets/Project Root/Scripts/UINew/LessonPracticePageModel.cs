using LessonsPractices;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

namespace UINew
{
    public abstract class LessonPracticePageModel
    {

        public event Action CheckClicked
        {

            add => _checkButton.clicked += value;

            remove => _checkButton.clicked -= value;
        }


        public event Action FinishClicked
        {

            add => _finishButton.clicked += value;

            remove => _finishButton.clicked -= value;
        }


        public event Action BackClicked
        {

            add => _backButton.clicked += value;

            remove => _backButton.clicked -= value;
        }


        protected readonly UIDocument document;


        private Button _checkButton;

        private Button _finishButton;

        private Button _backButton;


        public LessonPracticePageModel(UIDocument document)
        {
            this.document = document;
        }


        public abstract void SetInputs(LessonPractice practice, IList<InputField> fields);


        public void SetRoot(VisualTreeAsset root)
        {

            document.visualTreeAsset = root;

            UpdateElements();
        }


        public void SetCompleted(bool isCompleted)
        {

            _finishButton.SwapIf(isCompleted, "button__disabled", "button__success");
        }


        public void SetFieldReadOnly(TextField lineText, bool isReadOnly)
        {

            lineText.isReadOnly = isReadOnly;

            lineText.focusable = !isReadOnly;


            VisualElement textInput = lineText.GetElement("unity-text-input");

            textInput.SwapIf(isReadOnly, "input-not-readonly", "input-readonly");
        }


        protected virtual void UpdateElements()
        {

            _checkButton = document.GetButton("check-button");


            _finishButton = document.GetButton("finish-button");

            _finishButton.AddToClassList("button__disabled");


            _backButton = document.GetButton("back-button");
        }
    }
}
