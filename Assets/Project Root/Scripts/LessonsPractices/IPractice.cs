using System;

namespace LessonsPractices
{
    public interface IPractice
    {

        public event Action<bool> IsCompletedChanged;


        public void Init();

        public void StartGame();

        public bool IsCompleted();

        public void Unload();
    }
}
