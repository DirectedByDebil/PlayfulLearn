using Web;
using Lessons;
using Extensions;
using UnityEngine;
using System.Collections.Generic;

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

            UserData userData = SessionData.UserData;

            userData.CompletedLessons = GetCompletedLessons();


            string data = JsonUtility.ToJson(userData, true);
            

            FileExtensions.WriteFile(data, PathKeeper.UserDataPath);
        }


        private static List<string> GetCompletedLessons()
        {

            List<string> completed = new (SessionData.AllLessons.Count);


            foreach(Lesson lesson in SessionData.AllLessons)
            {

                if(lesson.IsCompleted)
                {

                    completed.Add(lesson.NameOfLesson);
                }
            }

            return completed;
        }
    }
}
