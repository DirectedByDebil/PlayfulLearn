using System;

namespace LessonsPractices
{
    public interface IMiniGame : IPractice
    {

        public event Action<bool, CodeInput> CodeChecked;

        public event Action<CodeInput> LockingInput;


        public void SetParams(params CodeInput[] inputs);
    }
}