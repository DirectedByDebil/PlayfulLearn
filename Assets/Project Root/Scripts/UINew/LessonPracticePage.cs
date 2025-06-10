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


        private List<InputField> _inputFields;


        private MiniGamePageModel _miniGamePageModel;

        private TestPageModel _testPageModel;

        private LessonPracticePageModel _currentPracticeModel;


        public override void Init()
        {
            
            base.Init();

            _inputFields = new List<InputField>();


            _miniGamePageModel = new MiniGamePageModel(document, _codeLineAsset);

            _testPageModel = new TestPageModel(document);
        }


        public void PreparePractice(LessonPractice practice)
        {

            SetPracticeContent(practice);


            PracticeChanged?.Invoke(practice);
        }


        #region On Input/Practice Checked

        public void OnInputChecked(bool isInputCorrect, TextField input)
        {

            input.SwapIf(isInputCorrect, "input-error", "input-correct");
        }


        public void OnPracticeChecked(bool isCompleted)
        {

            _currentPracticeModel.SetCompleted(isCompleted);
            

            if(isCompleted)
            {

                _currentPracticeModel.FinishClicked += OnFinishClicked;
            }
            else
            {

                _currentPracticeModel.FinishClicked -= OnFinishClicked;
            }
        }

        #endregion


        public void SetFieldReadOnly(TextField lineText, bool isReadOnly)
        {

            _currentPracticeModel.SetFieldReadOnly(lineText, isReadOnly);
        }


        private void SetPracticeContent(LessonPractice practice)
        {

            _inputFields.Clear();


            switch (practice.PracticeType)
            {

                case PracticeType.Test:



                    _testPageModel.SetRoot(practice.TestPage);


                    ChangeCurrentPracticeModel(_testPageModel);

                    break;


                case PracticeType.MiniGame:


                    _miniGamePageModel.SetRoot(_miniGamePage);

                    _miniGamePageModel.SetMiniGameInfo(practice.MiniGameInfo);

                    ChangeCurrentPracticeModel(_miniGamePageModel);

                    break;
            }


            _currentPracticeModel.SetInputs(practice, _inputFields);

            InputsChanged?.Invoke(_inputFields);
        }


        private void ChangeCurrentPracticeModel(LessonPracticePageModel newModel)
        {

            if(_currentPracticeModel != null)
            {

                _currentPracticeModel.CheckClicked -= OnCheckedClicked;

                _currentPracticeModel.FinishClicked -= OnFinishClicked;

                _currentPracticeModel.BackClicked -= StartClosing;
            }


            _currentPracticeModel = newModel;

            _currentPracticeModel.CheckClicked += OnCheckedClicked;

            _currentPracticeModel.BackClicked += StartClosing;
        }


        private void OnCheckedClicked() => CheckClicked?.Invoke();

        private void OnFinishClicked() => FinishClicked?.Invoke();
    }
}
