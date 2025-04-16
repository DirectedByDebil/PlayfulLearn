using Lessons;
using LearningPrograms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public sealed class ProgressSystem
    {

        public event Action ProgressChanging;

        private Lesson _currentLesson;


        public void OnLessonChanged(Lesson lesson)
        {

            _currentLesson = lesson;
        }


        public void OnLessonCompleting()
        {

            if (_currentLesson == null) return;


            if(!_currentLesson.IsCompleted)
            {

                _currentLesson.SetCompleted(true);

                ProgressChanging?.Invoke();
            }
        }


        public async Task CountProgressAsync(IReadOnlyCollection<LearningProgram> allPrograms)
        {

            foreach (LearningProgram program in allPrograms)
            {
                
                await program.CountProgressAsync();
            }
        }
    }
}
