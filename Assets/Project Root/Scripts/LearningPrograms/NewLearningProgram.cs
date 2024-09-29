using Lessons;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using Core;

namespace LearningPrograms
{
    public sealed class NewLearningProgram
    {

        public string NameOfProgram
        {

            get => _nameOfProgram;
        }


        public Sprite Icon
        {

            get => _sprite;
        }


        public IReadOnlyList<NewLesson> Lessons
        {

            get => _lessons;
        }


        private readonly string _nameOfProgram;

        private readonly string _iconName;


        private readonly List<string> _lessonsNames;

        private readonly List<NewLesson> _lessons;


        private Sprite _sprite;


        public NewLearningProgram(LearningProgramData data)
        {

            _nameOfProgram = data.NameOfProgram;

            _iconName = data.IconName;

            _lessonsNames = data.Lessons;


            _lessons = new List<NewLesson>(_lessonsNames.Count);
        }


        public void AddLessons(IEnumerable<NewLesson> allLessons)
        {

            foreach(NewLesson lesson in allLessons)
            {

                if(_lessonsNames.Contains(lesson.NameOfLesson))
                {

                    _lessons.Add(lesson);
                }
            }
        }


        public void LoadIcon()
        {

            string fileName = 
                
                PathKeeper.GetLearningProgramIconFileName(_iconName);


            if(ImageExtensions.TryCreateSprite(fileName,
                
                out Sprite sprite))
            {

                _sprite = sprite;
            }
        }
    }
}
