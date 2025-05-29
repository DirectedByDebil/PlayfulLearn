using UnityEngine.UIElements;
using System;
using System.Collections.Generic;


namespace LessonsPractices
{
    public class LessonPracticeModel
    {

        public event Action<bool, TextField> InputChecked;

        public event Action<TextField> LockingInput;

        public event Action<bool> PracticeChecked;

        public event Action<string> MiniGameLoading;


        private IReadOnlyList<InputField> _inputFields;


        private LessonPractice _currentPractice;

        private IMiniGame _currentMiniGame;


        public void OnInputsChanged(IReadOnlyList<InputField> inputs)
        {

            _inputFields = inputs;
        }


        public void OnMiniGameFound(IMiniGame miniGame)
        {

            _currentMiniGame = miniGame;

            _currentMiniGame.CodeChecked += OnCodeChecked;

            _currentMiniGame.IsCompletedChanged += OnMiniGameCompletedChanged;

            _currentMiniGame.LockingInput += OnLockingInput;
            
            _currentMiniGame.Init();
        }


        public void UnloadMiniGame()
        {

            if (_currentMiniGame != null)
            {

                _currentMiniGame.CodeChecked -= OnCodeChecked;

                _currentMiniGame.IsCompletedChanged -= OnMiniGameCompletedChanged;

                _currentMiniGame.LockingInput -= OnLockingInput;

                _currentMiniGame.Unload();


                _currentMiniGame = null;
            }
        }


        #region Set/Check Practice

        public void SetPractice(LessonPractice lessonPractice)
        {

            _currentPractice = lessonPractice;


            if(_currentPractice.PracticeType == PracticeType.MiniGame)
            {

                MiniGameLoading?.Invoke(_currentPractice.SceneName);
            }
        }


        public void OnCheckingPractice()
        {

            bool isCompleted = false;

            switch (_currentPractice.PracticeType)
            {

                case PracticeType.Test:

                    isCompleted = IsTestCompleted();
                    break;


                case PracticeType.MiniGame:

                    isCompleted = IsMiniGameCompleted();
                    break;
            }

            PracticeChecked?.Invoke(isCompleted);
        }

        #endregion


        #region Test

        private bool IsTestCompleted()
        {

            bool isCompleted = true;
            

            IReadOnlyList<Question> questions = _currentPractice.Questions;


            for (int i = 0; i < questions.Count; i++)
            {

                TextField input = _inputFields[i].TextField;


                bool isInputCorrect = input.value == questions[i].Answer;

                if (!isInputCorrect)
                {

                    isCompleted = false;
                }

                InputChecked?.Invoke(isInputCorrect, input);
            }


            return isCompleted;
        }

        #endregion


        #region Mini Game

        private bool IsMiniGameCompleted()
        {

            _currentMiniGame.SetParams(GetCodeInputs());

            _currentMiniGame.StartGame();


            return _currentMiniGame.IsCompleted();
        }


        private CodeInput[] GetCodeInputs()
        {

            CodeInput[] inputs = new CodeInput[_inputFields.Count];


            for(int i = 0; i < _inputFields.Count; i++)
            {

                InputField field = _inputFields[i];

                inputs[i] = new()
                {

                    Code = field.TextField.text,

                    Description = field.Description
                };
            }

            return inputs;
        }


        private void OnCodeChecked(bool isValid, CodeInput input)
        {

            if(TryFindTextField(input.Description, out TextField field))
            {

                InputChecked?.Invoke(isValid, field);
            }
        }


        private void OnMiniGameCompletedChanged(bool isCompleted)
        {

            PracticeChecked?.Invoke(isCompleted);
        }


        private void OnLockingInput(CodeInput input)
        {

            if(TryFindTextField(input.Description, out TextField field))
            {

                LockingInput?.Invoke(field);
            }
        }


        private bool TryFindTextField(string description, out TextField textField)
        {

            foreach (InputField field in _inputFields)
            {

                if (field.Description == description)
                {

                    textField = field.TextField;

                    return true;
                }
            }

            textField = null;

            return false;
        }

        #endregion
    }
}
