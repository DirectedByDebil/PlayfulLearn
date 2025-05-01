using Lessons;
using LearningPrograms;
using Web;
using Playables;
using System.Collections.Generic;

namespace Core
{
    public static class SessionData
    {

        public static IReadOnlyCollection<Lesson> AllLessons
        {

            get => _allLessons;
        }


        public static List<LearningProgram> AllLearningPrograms
        {

            get => _allLearningPrograms;
        }


        public static IReadOnlyCollection<Character> Characters { get; set; }


        public static LearningProgram LastLearningProgram
        {

            get => _lastLearningProgram;
        }


        public static UserData UserData
        {

            get => _userData;
        }


        public static Character SelectedCharacter
        {
            get=> _selectedCharacter;
        }



        private static List<Lesson> _allLessons;


        private static List<LearningProgram> _allLearningPrograms;


        private static LearningProgram _lastLearningProgram;


        private static UserData _userData;


        private static Character _selectedCharacter;


        #region Setters

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


        public static void SetUserData(UserData userData)
        {

            _userData = userData;
        }


        public static void SetCharacter(Character character)
        {

            _selectedCharacter = character;

            _userData.SelectedCharacter = _selectedCharacter.CharacterType.ToString();
        }

        #endregion


        #region Add Methods

        public static void AddLesson(Lesson lesson)
        {

            _allLessons.Add(lesson);
        }


        public static void AddLearningProgram(LearningProgram program)
        {

            _allLearningPrograms.Add(program);
        }
        
        #endregion
    }
}
