using Lessons;
using LearningPrograms;
using System;
using System.Collections.Generic;

namespace Web
{
    [Serializable]
    public struct InitData
    {

        public UserData User;

        public string StartingModule;

        public List<LessonData> Lessons;

        public List<LearningProgramData> LearningPrograms;
    }
}
