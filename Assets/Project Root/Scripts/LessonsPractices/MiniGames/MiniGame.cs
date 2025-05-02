using UnityEngine;
using System;

namespace LessonsPractices.MiniGames
{
    public abstract class MiniGame : MonoBehaviour, IMiniGame
    {

        public abstract event Action<bool> IsCompletedChanged;

        public event Action<bool, CodeInput> CodeChecked;

        public event Action<CodeInput> LockingInput;


        protected bool CanStart { get => _isInputValid; }


        private bool _isInputValid;


        public void SetParams(params CodeInput[] inputs)
        {

            foreach (CodeInput input in inputs)
            {

                CheckCodeInput(input);
            }
        }


        public virtual bool IsCompleted()
        {

            if(!_isInputValid) return false;

            return CountIsCompleted();
        }


        #region Abstract Methods

        public abstract void StartGame();

        protected abstract bool CountIsCompleted();

        protected abstract void CheckCodeInput(CodeInput input);

        #endregion


        #region Input Events

        protected void InputSucceed(CodeInput input)
        {

            _isInputValid = true;

            CodeChecked?.Invoke(true, input);
        }


        protected void InputFailed(CodeInput input)
        {

            _isInputValid = false;

            CodeChecked?.Invoke(false, input);
        }


        protected void LockInput(CodeInput input)
        {

            LockingInput?.Invoke(input);
        }

        #endregion


        #region Check/Clean Input Methods

        protected bool IsInputValid(string code, string startPhrase)
        {

            _isInputValid = IsInputValid(code, startPhrase, ";");
            
            return _isInputValid;
        }


        protected bool IsInputValid(string code, string startPhrase, string endPhrase)
        {

            _isInputValid = code.StartsWith(startPhrase) && code.EndsWith(endPhrase);

            return _isInputValid;
        }


        protected void CleanInput(ref string input)
        {

            input = input.Replace(" ", "");

            input = input.Replace(";", "");
        }


        protected bool TryReadValue(string input, string odd, out float value)
        {

            value = 0;

            if (input.Contains(odd))
            {

                input = input.Replace(odd, "");


                return float.TryParse(input, out value);
            }

            return false;
        }

        #endregion
    }
}
