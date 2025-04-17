using Extensions;
using UnityEngine;

namespace Core
{
    public static class App
    {

        public static void Save()
        {

            SaveLastLearningProgram();

            SaveUserData();
        }


        private static void SaveLastLearningProgram()
        {

            string name = SessionData.LastLearningProgram.NameOfProgram;


            FileExtensions.WriteFile(name, PathKeeper.LastLearningProgramName);
        }


        private static void SaveUserData()
        {

            string data = JsonUtility.ToJson(SessionData.UserData, true);
            

            string fileName = string.Format("{0}/{1}.json", PathKeeper.UserDataPath, "User");


            FileExtensions.WriteFile(data, fileName);
        }
    }
}
