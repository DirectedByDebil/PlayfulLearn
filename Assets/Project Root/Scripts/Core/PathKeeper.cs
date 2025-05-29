namespace Core
{
    public static class PathKeeper
    {

        public static string LessonsPath
        {

            get => _root + "Lessons";
        }


        public static string LearningProgramsPath
        {

            get => _root + "Learning Programs";
        }


        public static string LessonsIconPath
        {

            get => _root + "Graphics/Icons/Lessons";
        }


        public static string LearningProgramsIconPath
        {

            get => _root + "Graphics/Icons/Learning Programs";
        }


        public static string LastLearningProgramName
        {

            get => LearningProgramsPath + "/Last Learning Program.txt";
        }


        public static string UserDataPath
        {
            get => _root + "User/User.json";
        }


        public static string InitDataPath
        {
            get => _root + "Bootstrap/Init.json";
        }


        private static string _root;


        public static void SetRoot(string root)
        {
            
            _root = root;
        }
    }
}
