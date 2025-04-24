using UnityEngine.UIElements;
using System;
using System.Collections.Generic;


namespace LessonsPractices
{
    public class LessonPracticeModel
    {

        public event Action<bool, TextField> InputChecked;

        public event Action<bool> PracticeChecked;

        public event Action<string> MiniGameLoading;


        private IReadOnlyList<TextField> _inputs;


        private LessonPractice _currentPractice;

        private IMiniGame _currentMiniGame;


        public void OnInputsChanged(IReadOnlyList<TextField> inputs)
        {

            _inputs = inputs;
        }


        public void OnMiniGameFound(IMiniGame miniGame)
        {

            _currentMiniGame = miniGame;
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

                TextField input = _inputs[i];


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

            _currentMiniGame.SetParams();

            _currentMiniGame.StartGame();


            return _currentMiniGame.IsCompleted();
        }

        #endregion
    }
}
