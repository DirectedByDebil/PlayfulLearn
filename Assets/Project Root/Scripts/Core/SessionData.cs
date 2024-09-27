using System.Collections.Generic;
using Lessons;
using LearningPrograms;

namespace Core
{
    public static class SessionData
    {

        public static IReadOnlyCollection<

            NewLesson> AllLessons
        {

            get => _allLessons;
        }


        public static IReadOnlyCollection<

            NewLearningProgram> AllLearningPrograms
        {

            get => _allLearningPrograms;
        }



        private static IReadOnlyList<
            
            NewLesson> _allLessons;


        private static IReadOnlyList<
            
            NewLearningProgram> _allLearningPrograms;



        public static void SetAllLessons(IReadOnlyList<
            
            NewLesson> allLessons)
        {

            _allLessons = allLessons;
        }


        public static void SetAllLearningPrograms(IReadOnlyList<
            
            NewLearningProgram> allLearningPrograms)
        {

            _allLearningPrograms = allLearningPrograms;
        }
    }
}
