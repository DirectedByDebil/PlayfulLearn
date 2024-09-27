using System.Collections.Generic;
using System;

namespace LearningPrograms
{

    [Serializable]
    public struct LearningProgramData
    {

        public string NameOfProgram;

        public string IconName;

        public List<string> Lessons;


        public LearningProgramData(string nameOfProgram,

            string iconName,
            
            List<string> lessons)
        {

            NameOfProgram = nameOfProgram;

            IconName = iconName;

            Lessons = lessons;
        }
    }
}