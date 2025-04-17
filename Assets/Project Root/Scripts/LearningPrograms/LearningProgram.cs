using Lessons;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using Core;
using System.Threading.Tasks;

namespace LearningPrograms
{
    public sealed class LearningProgram
    {

        public string NameOfProgram
        {

            get => _nameOfProgram;
        }


        public int CompletionPercent
        {

            get => _completionPercent;
        }


        public Sprite Icon
        {

            get => _sprite;
        }


        public IReadOnlyList<Lesson> Lessons
        {

            get => _lessons;
        }


        private readonly string _nameOfProgram;

        private readonly string _iconName;


        private readonly List<string> _lessonsNames;

        private readonly List<Lesson> _lessons;


        private Sprite _sprite;

        private int _completionPercent;


        public LearningProgram(LearningProgramData data)
        {

            _nameOfProgram = data.NameOfProgram;

            _iconName = data.IconName;

            _lessonsNames = data.Lessons;


            _lessons = new List<Lesson>(_lessonsNames.Count);
        }


        public async Task AddLessonsAsync(IEnumerable<Lesson> allLessons)
        {

            //await Task.Run(() =>
            //{
            
                foreach(Lesson lesson in allLessons)
                {

                    if(_lessonsNames.Contains(lesson.NameOfLesson))
                    {

                        _lessons.Add(lesson);
                    }
                }
            //});
        }


        public void LoadIcon()
        {

            string fileName = string.Format("{0}/{1}",

                PathKeeper.LearningProgramsIconPath, _iconName);
            

            if(ImageExtensions.TryCreateSprite(fileName,
                
                out Sprite sprite))
            {

                _sprite = sprite;
            }
        }


        public async Task CountProgressAsync()
        {

            int percent = 0;

            //await Task.Run(() =>
            //{

                List<Lesson> completedLessons = _lessons.FindAll((lesson) => lesson.IsCompleted);
    
                percent = 100 * completedLessons.Count / _lessons.Count;
            //});


            _completionPercent = percent;
        }
    }
}
