using Web;
using Extensions;
using Lessons;
using LearningPrograms;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Core
{
    public sealed class InitScene : MonoBehaviour
    {

        [SerializeField, Space]

        private TextMeshProUGUI _statusHandler;
        

        [SerializeField, Space]

        private string _menuSceneName;


        private string _lastProgramName;


        private void Awake()
        {

            InitUser();


            InitializeLessons();


            InitializeLastProgram();

            InitializeLearningProgramsAsync();



            _statusHandler.text = "Ready";


            SceneManager.LoadSceneAsync(_menuSceneName);
        }


        private void InitUser()
        {

            UserData user = FileExtensions.GetFromFile<UserData>(PathKeeper.UserDataPath);

            SessionData.SetUserData(user);
        }


        private void InitializeLessons()
        {

            _statusHandler.text = "Loading lessons";


            List<string> completed = SessionData.UserData.CompletedLessons;


            IReadOnlyCollection<LessonData> allLessonsData =
                
                FileExtensions.GetFiles<LessonData>(PathKeeper.LessonsPath);


            List<Lesson> allLessons = new(allLessonsData.Count);


            foreach(LessonData data in allLessonsData)
            {

                Lesson lesson = new (data);

                lesson.InitializeContent();

                lesson.LoadIcon();


                if(completed.Contains(lesson.NameOfLesson))
                {

                    lesson.SetCompleted(true);

                    completed.Remove(lesson.NameOfLesson);
                }


                allLessons.Add(lesson);
            }


            SessionData.SetAllLessons(allLessons);
        }


        private void InitializeLastProgram()
        {

            if(FileExtensions.TryReadFile(
                
                PathKeeper.LastLearningProgramName,
                
                out string result))
            {

                _lastProgramName = result;
            }
        }


        private async Task InitializeLearningProgramsAsync()
        {

            _statusHandler.text = "Loading learning programs";


            IReadOnlyCollection<LearningProgramData> allProgramsData =

                FileExtensions.GetFiles<LearningProgramData>(PathKeeper.LearningProgramsPath);


            List<LearningProgram> allPrograms = new(allProgramsData.Count);


            foreach (LearningProgramData data in allProgramsData)
            {

                LearningProgram program = new(data);

                program.LoadIcon();

                program.AddLessonsAsync(SessionData.AllLessons);

                program.CountProgressAsync();


                allPrograms.Add(program);
            }


            SessionData.SetAllLearningPrograms(allPrograms);



            LearningProgram last =
                
                allPrograms.Find(FindLastLearningProgram);


            SessionData.SetLastLearningProgram(last);
        }


        private bool FindLastLearningProgram(LearningProgram learningProgram)
        {

            return learningProgram.NameOfProgram == _lastProgramName;
        }
    }
}