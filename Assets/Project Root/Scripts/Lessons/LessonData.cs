using System;
using System.Collections.Generic;
using Localization;

namespace Lessons
{
    [Serializable]
    public struct LessonData
    {

        public string NameOfLesson;

        public List<LessonNode> Contents;


        public LessonData(string nameOfLesson,
            
            List<LessonNode> contents)
        {

            NameOfLesson = nameOfLesson;

            Contents = contents;
        }
    }
}
