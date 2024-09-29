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


        public static string LastLearningProgramName
        {

            get => LearningProgramsPath + "/Last Learning Program.txt";
        }


        public static string GetLessonsIconFileName(string iconName)
        {

            return string.Format(

                "Assets/Project Root/Graphics/Icons/Lessons/{0}", iconName);
        }


        public static string GetLearningProgramIconFileName(string iconName)
        {

            return string.Format(

                "Assets/Project Root/Graphics/Icons/Learning Programs/{0}", iconName);
        }
    }
}
