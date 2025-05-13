using System.Collections.Generic;
using System;

namespace LearningPrograms
{

    [Serializable]
    public struct LearningProgramData
    {

        public string NameOfProgram;

        public string RusName;

        public string IconName;

        public int Module;

        public List<string> Lessons;



        public LearningProgramData(string nameOfProgram,

            string iconName,
            
            List<string> lessons)
        {

            NameOfProgram = nameOfProgram;

            RusName = nameOfProgram;

            IconName = iconName;

            Lessons = lessons;

            Module = 0;
        }
    }
}