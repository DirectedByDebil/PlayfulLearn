using System.Collections.Generic;
using Lessons;

namespace LearningPrograms
{
    public struct LearningProgramTemplate
    {

        public List<Lesson> lessons;


        public LearningProgramTemplate(List<Lesson> lessons)
        {

            this.lessons = lessons;
        }
    }
}