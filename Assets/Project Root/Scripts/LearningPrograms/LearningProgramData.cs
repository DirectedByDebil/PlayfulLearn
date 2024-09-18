using System.Collections.Generic;
using Lessons;
using System;

namespace LearningPrograms
{

    [Serializable]
    public struct LearningProgramData
    {

        public string name;

        public List<string> lessons;


        public LearningProgramData(string name, 
            
            List<string> lessons)
        {

            this.name = name;

            this.lessons = lessons;
        }
    }
}