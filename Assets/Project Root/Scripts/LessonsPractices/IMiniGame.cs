using System;

namespace LessonsPractices
{
    public interface IMiniGame
    {

        public event Action<bool, CodeInput> CodeChecked;

        public event Action<bool> IsCompletedChanged;

        public event Action<CodeInput> LockingInput;


        public void Init();

        public void StartGame();

        public void SetParams(params CodeInput[] inputs);

        public bool IsCompleted();

        public void Unload();
    }
}