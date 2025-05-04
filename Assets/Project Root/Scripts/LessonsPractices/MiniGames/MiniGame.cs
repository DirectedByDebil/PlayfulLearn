using UnityEngine;
using System;
using System.Collections.Generic;

namespace LessonsPractices.MiniGames
{
    public abstract class MiniGame : MonoBehaviour, IMiniGame
    {

        public abstract event Action<bool> IsCompletedChanged;

        public event Action<bool, CodeInput> CodeChecked;

        public event Action<CodeInput> LockingInput;


        protected bool CanStart
        {
            get => _inputsToCheck?.Count == 0; 
        }


        private IList<string> _inputsToCheck;


        public void SetParams(params CodeInput[] inputs)
        {

            _inputsToCheck.Clear();

            
            foreach (CodeInput input in inputs)
            {

                _inputsToCheck.Add(input.Description);

                CheckCodeInput(input);
            }
        }


        public virtual bool IsCompleted()
        {

            if(!CanStart) return false;

            return CountIsCompleted();
        }
  
        
        public virtual void Init()
        {

            _inputsToCheck = new List<string>();
        }


        #region Abstract Methods

        public abstract void Unload();

        public abstract void StartGame();

        protected abstract bool CountIsCompleted();

        protected abstract void CheckCodeInput(CodeInput input);

        #endregion


        #region Input Events

        protected void InputSucceed(CodeInput input)
        {

            _inputsToCheck.Remove(input.Description);

            CodeChecked?.Invoke(true, input);
        }


        protected void InputFailed(CodeInput input)
        {
            
            _inputsToCheck.Add(input.Description);

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

            return IsInputValid(code, startPhrase, ";");
        }


        protected bool IsInputValid(string code, string startPhrase, string endPhrase)
        {

            return code.StartsWith(startPhrase) && code.EndsWith(endPhrase);
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
