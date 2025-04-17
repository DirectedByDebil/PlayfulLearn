namespace Core
{
    public static class PathKeeper
    {

        public static string LessonsPath
        {

            get => "Assets/Project Root/Lessons";
        }


        public static string LearningProgramsPath
        {

            get => "Assets/Project Root/Learning Programs";
        }


        public static string LessonsIconPath
        {

            get => "Assets/Project Root/Graphics/Icons/Lessons";
        }


        public static string LearningProgramsIconPath
        {

            get => "Assets/Project Root/Graphics/Icons/Learning Programs";
        }


        public static string LastLearningProgramName
        {

            get => LearningProgramsPath + "/Last Learning Program.txt";
        }


        public const string UserDataPath = "Assets/Project Root/User";
    }
}
