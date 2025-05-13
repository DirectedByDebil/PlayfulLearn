using Core;
using Lessons;
using Extensions;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningPrograms
{
    public sealed class LearningProgram : IComparable<LearningProgram>
    {

        public string NameOfProgram
        {

            get => _nameOfProgram;
        }

        public string RusName { get => _rusName; }


        public int Module { get => _module; }

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

        private readonly string _rusName;

        private readonly string _iconName;

        private readonly int _module;


        private readonly List<string> _lessonsNames;

        private readonly List<Lesson> _lessons;


        private Sprite _sprite;

        private int _completionPercent;


        public LearningProgram(LearningProgramData data)
        {

            _nameOfProgram = data.NameOfProgram;

            _iconName = data.IconName;

            _lessonsNames = data.Lessons;

            _rusName = data.RusName;

            _module = data.Module;


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


        public int CompareTo(LearningProgram other)
        {
            return Module.CompareTo(other.Module);
        }
    }
}
