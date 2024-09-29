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


        public static NewLearningProgram LastLearningProgram
        {

            get => _lastLearningProgram;
        }



        private static IReadOnlyList<
            
            NewLesson> _allLessons;


        private static IReadOnlyList<
            
            NewLearningProgram> _allLearningPrograms;


        private static NewLearningProgram _lastLearningProgram;


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


        public static void SetLastLearningProgram(
            
            NewLearningProgram lastLearningProgram)
        {

            _lastLearningProgram = lastLearningProgram;
        }
    }
}
