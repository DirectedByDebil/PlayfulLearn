using System;
using System.Collections.Generic;
using Localization;

namespace Lessons
{
    [Serializable]
    public struct LessonData
    {

        public string NameOfLesson;

        public string IconName;

        public string Difficulty;

        public List<LessonNode> Contents;


        public LessonData(string nameOfLesson,

            string iconName,
            
            List<LessonNode> contents)
        {

            NameOfLesson = nameOfLesson;

            IconName = iconName;

            Contents = contents;

            Difficulty = "Easy";
        }
    }
}
