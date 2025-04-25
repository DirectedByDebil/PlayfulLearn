using Lessons;
using LessonsPractices;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

namespace UINew
{
    public sealed class LessonPracticePage : Page
    {

        public event Action CheckClicked;

        public event Action FinishClicked;

        public event Action<LessonPractice> PracticeChanged;

        public event Action<IReadOnlyList<InputField>> InputsChanged;


        [SerializeField, Space]
        private List<LessonPractice> _practices;


        private Button _checkButton;

        private Button _finishButton;


        private List<InputField> _inputFields;


        public override void Init()
        {
            
            base.Init();

            _inputFields = new List<InputField>();
        }


        public void PreparePractice(Lesson lesson)
        {

            //#TODO maybe better do with binary search
            LessonPractice lessonPractice = _practices.Find(
                (practice) => practice.NameOfLesson == lesson.NameOfLesson);

            document.visualTreeAsset = lessonPractice.Asset;
            

            SetButtons();

            SetInputs(lessonPractice);

            PracticeChanged?.Invoke(lessonPractice);
        }


        #region On Input/Practice Checked

        public void OnInputChecked(bool isInputCorrect, TextField input)
        {

            input.SwapIf(isInputCorrect, "input-error", "input-correct");
        }


        public void OnPracticeChecked(bool isCompleted)
        {

            _finishButton.SwapIf(isCompleted, "button__disabled", "border-success-colored");


            if(isCompleted)
            {

                _finishButton?.RegisterCallback<ClickEvent>(OnFinishClicked);
            }
            else
            {

                _finishButton?.UnregisterCallback<ClickEvent>(OnFinishClicked);
            }
        }

        #endregion


        #region Set Inputs

        private void SetInputs(LessonPractice practice)
        {

            _inputFields.Clear();


            switch (practice.PracticeType)
            {

                case PracticeType.Test:

                    SetTestInputs(practice);
                    break;


                case PracticeType.MiniGame:

                    SetCodeInputs(practice);
                    break;
            }


            InputsChanged?.Invoke(_inputFields);
        }


        private void SetTestInputs(LessonPractice practice)
        {

            foreach (Question question in practice.Questions)
            {

                InputField textField = GetTestInput(question);

                _inputFields.Add(textField);
            }
        }


        private InputField GetTestInput(Question question)
        {

            VisualElement element = document.GetElement(question.VisualElementName);


            VisualElement picture = element.GetElement("picture");

            picture.style.backgroundImage = new StyleBackground(question.Picture);


            InputField field = new()
            {

                TextField = element.GetTextField("input-field")
            };


            return field;
        }


        private void SetCodeInputs(LessonPractice practice)
        {

            VisualElement codeRoot = document.GetElement("code-root");


            List<VisualElement> children = new (codeRoot.Children());
           

            for(int i = 0; i < children.Count; i++)
            {

                CodeLine codeLine = practice.CodeLines[i];

                InputField field = GetCodeInput(children[i], codeLine, i+1);
                

                if(!string.IsNullOrEmpty(codeLine.Description))
                {

                    _inputFields.Add(field);
                }
            }
        }


        private InputField GetCodeInput(VisualElement root,
            CodeLine codeLine, int lineNumber)
        {

            Label lineNumberLabel = root.GetLabel("line-number");

            lineNumberLabel.text = lineNumber.ToString();


            TextField lineText = root.GetTextField("line-text");

            lineText.value = codeLine.Code;


            bool isReadonly = codeLine.IsReadOnly;

            lineText.isReadOnly = isReadonly;

            lineText.focusable = !isReadonly;


            VisualElement textInput = lineText.GetElement("unity-text-input");

            textInput.SwapIf(isReadonly, "input-not-readonly", "input-readonly");


            InputField field = new()
            {
                TextField = lineText,

                Description = codeLine.Description
            };

            return field;
        }

        #endregion


        #region Set Buttons

        private void SetButtons()
        {

            _checkButton = document.GetButton("check-button");

            _checkButton?.RegisterCallback<ClickEvent>(OnCheckClicked);
            

            _finishButton = document.GetButton("finish-button");

            _finishButton.AddToClassList("button__disabled");
        }


        private void OnCheckClicked(ClickEvent e)
        {

            CheckClicked?.Invoke();
        }


        private void OnFinishClicked(ClickEvent e)
        {

            _checkButton.UnregisterCallback<ClickEvent>(OnCheckClicked);

            _finishButton.UnregisterCallback<ClickEvent>(OnFinishClicked);


            FinishClicked?.Invoke();
        }

        #endregion
    }
}
