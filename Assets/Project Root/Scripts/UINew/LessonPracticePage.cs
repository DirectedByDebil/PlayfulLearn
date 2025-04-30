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
        private VisualTreeAsset _miniGamePage;

        [SerializeField, Space]
        private VisualTreeAsset _codeLineAsset;


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


            SetPracticeContent(lessonPractice);

            SetButtons();


            PracticeChanged?.Invoke(lessonPractice);
        }


        #region On Input/Practice Checked

        public void OnInputChecked(bool isInputCorrect, TextField input)
        {

            input.SwapIf(isInputCorrect, "input-error", "input-correct");
        }


        public void OnPracticeChecked(bool isCompleted)
        {

            _finishButton.SwapIf(isCompleted, "button__disabled", "button__success");


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


        public void SetFieldReadOnly(TextField lineText, bool isReadOnly)
        {

            lineText.isReadOnly = isReadOnly;

            lineText.focusable = !isReadOnly;


            VisualElement textInput = lineText.GetElement("unity-text-input");

            textInput.SwapIf(isReadOnly, "input-not-readonly", "input-readonly");
        }


        private void SetPracticeContent(LessonPractice practice)
        {

            _inputFields.Clear();


            switch (practice.PracticeType)
            {

                case PracticeType.Test:


                    document.visualTreeAsset = practice.TestPage;

                    SetTestInputs(practice);
                    break;


                case PracticeType.MiniGame:


                    document.visualTreeAsset = _miniGamePage;

                    SetMiniGameInfo(practice.MiniGameInfo);

                    SetCodeInputs(practice);
                    break;
            }

            InputsChanged?.Invoke(_inputFields);
        }


        private void SetMiniGameInfo(VisualTreeAsset infoAsset)
        {

            VisualElement info = infoAsset.Instantiate();

            info.AddToClassList("mini-game-info");


            VisualElement body = document.GetElement("body");

            body.Insert(0, info);
        }


        #region Set Inputs

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

            codeRoot.Clear();


            for(int i = 0; i < practice.CodeLines.Count; i++)
            {

                VisualElement element = _codeLineAsset.Instantiate();


                CodeLine codeLine = practice.CodeLines[i];

                InputField field = GetCodeInput(element, codeLine, i+1);
                

                if(!string.IsNullOrEmpty(codeLine.Description))
                {

                    _inputFields.Add(field);
                }

                codeRoot.Add(element);
            }
        }


        private InputField GetCodeInput(VisualElement root,
            CodeLine codeLine, int lineNumber)
        {

            Label lineNumberLabel = root.GetLabel("line-number");

            lineNumberLabel.text = lineNumber.ToString();


            TextField lineText = root.GetTextField("line-text");

            lineText.value = codeLine.Code;


            SetFieldReadOnly(lineText, codeLine.IsReadOnly);
            

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
