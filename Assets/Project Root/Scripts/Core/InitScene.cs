using UnityEngine;
using Extensions;
using Lessons;
using LearningPrograms;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

namespace Core
{
    public sealed class InitScene : MonoBehaviour
    {

        [SerializeField, Space]

        private TextMeshProUGUI _statusHandler;


        [SerializeField, Space]

        private string _menuSceneName;


        private string _lessonsPath = 
            
            "Assets/Project Root/Lessons";
        

        private string _learningProgramsPath = 
            
            "Assets/Project Root/Learning Programs";



        private void Awake()
        {

            _statusHandler.text = "Loading lessons";

            InitializeLessons();


            _statusHandler.text = "Loading learning programs";

            InitializeLearningPrograms();


            _statusHandler.text = "Ready";


            SceneManager.LoadSceneAsync(_menuSceneName);
        }


        private void InitializeLessons()
        {

            IReadOnlyCollection<LessonData> allLessonsData =
                
                FileExtensions.GetFiles<LessonData>(_lessonsPath);


            List<NewLesson> allLessons = new(allLessonsData.Count);


            foreach(LessonData data in allLessonsData)
            {

                NewLesson lesson = new (data);

                lesson.InitializeContent();

                lesson.LoadIcon();


                allLessons.Add(lesson);
            }


            SessionData.SetAllLessons(allLessons);
        }


        private void InitializeLearningPrograms()
        {

            IReadOnlyCollection<LearningProgramData> allProgramsData =

                FileExtensions.GetFiles<LearningProgramData>(_learningProgramsPath);


            List<NewLearningProgram> allPrograms = new(allProgramsData.Count);


            foreach (LearningProgramData data in allProgramsData)
            {

                NewLearningProgram program = new(data);

                program.LoadIcon();

                program.AddLessons(SessionData.AllLessons);


                allPrograms.Add(program);
            }


            SessionData.SetAllLearningPrograms(allPrograms);
        }
    }
}