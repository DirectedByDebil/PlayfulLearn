using System.Collections.Generic;
using Lessons;
using LearningPrograms;

namespace Core
{
    public static class SessionData
    {

        public static IReadOnlyCollection<

            Lesson> AllLessons
        {

            get => _allLessons;
        }


        public static List<LearningProgram> AllLearningPrograms
        {

            get => _allLearningPrograms;
        }


        public static LearningProgram LastLearningProgram
        {

            get => _lastLearningProgram;
        }



        private static List<Lesson> _allLessons;


        private static List<LearningProgram> _allLearningPrograms;


        private static LearningProgram _lastLearningProgram;


        public static void SetAllLessons(IReadOnlyList<
            
            Lesson> allLessons)
        {

            _allLessons = new List<Lesson>(allLessons);
        }


        public static void SetAllLearningPrograms(IReadOnlyList<
            
            LearningProgram> allLearningPrograms)
        {

            _allLearningPrograms = new List<LearningProgram>
                
                (allLearningPrograms);
        }


        public static void SetLastLearningProgram(
            
            LearningProgram lastLearningProgram)
        {

            _lastLearningProgram = lastLearningProgram;
        }


        public static void AddLesson(Lesson lesson)
        {

            _allLessons.Add(lesson);
        }


        public static void AddLearningProgram(LearningProgram program)
        {

            _allLearningPrograms.Add(program);
        }
    }
}
