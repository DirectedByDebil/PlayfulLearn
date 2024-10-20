using Lessons;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using Core;

namespace LearningPrograms
{
    public sealed class LearningProgram
    {

        public string NameOfProgram
        {

            get => _nameOfProgram;
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


        public LearningProgram(LearningProgramData data)
        {

            _nameOfProgram = data.NameOfProgram;

            _iconName = data.IconName;

            _lessonsNames = data.Lessons;


            _lessons = new List<Lesson>(_lessonsNames.Count);
        }


        public void AddLessons(IEnumerable<Lesson> allLessons)
        {

            foreach(Lesson lesson in allLessons)
            {

                if(_lessonsNames.Contains(lesson.NameOfLesson))
                {

                    _lessons.Add(lesson);
                }
            }
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
    }
}
