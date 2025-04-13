using Core;
using Lessons;
using LearningPrograms;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace UINew
{
    public sealed class MainMenuObserver : MonoBehaviour
    {

        [SerializeField, Space]
        private CurrentLearningProgramPage _currentProgramPage;


        [SerializeField, Space]
        private AllLearningProgramsPage _allProgramsPage;


        [SerializeField, Space]
        private UserAccountPage _userAccountPage;

        [SerializeField, Space]
        private LessonPage _lessonPage;


        private Lesson _currentLesson;


        private void Start()
        {

            Init();


            _currentProgramPage.ViewLearningProgram(
                SessionData.LastLearningProgram);


            List<LearningProgram> learningPrograms = 
                new(SessionData.AllLearningPrograms);

            _allProgramsPage.ViewLearningPrograms(learningPrograms);
        }


        private void OnEnable()
        {
            
            SetObserver();
        }


        public void Init()
        {

            _currentProgramPage.Init();

            _allProgramsPage.Init();

            _userAccountPage.Init();

            _lessonPage.Init();


            _allProgramsPage.Hide();

            _userAccountPage.Hide();

            _lessonPage.Hide();
        }


        public void SetObserver()
        {

            _currentProgramPage.OpenLearningProgramsClicked +=
                _allProgramsPage.Show;


            _currentProgramPage.UserAccountClicked +=
                _userAccountPage.Show;


            _currentProgramPage.LessonClicked += OnLessonClicked;


            _userAccountPage.CloseClicked += _userAccountPage.Hide;

            _allProgramsPage.CloseClicked += _allProgramsPage.Hide;


            _allProgramsPage.LearningProgramClicked += OnLearningProgramClicked;


            _lessonPage.BackClicked += OnLessonBackClicked;

            _lessonPage.StartClicked += OnStartPracticeClicked;
        }


        private void OnLearningProgramClicked(LearningProgram program)
        {

            _currentProgramPage.ViewLearningProgram(program);

            _allProgramsPage.Hide();
        }


        private void OnLessonClicked(Lesson lesson)
        {

            _currentLesson = lesson;


            _currentProgramPage.Hide();


            _lessonPage.Show();

            _lessonPage.ViewLesson(lesson);
        }


        private void OnLessonBackClicked()
        {

            _lessonPage.Hide();

            _currentProgramPage.Show();
        }


        private void OnStartPracticeClicked()
        {

            Debug.Log(_currentLesson?.NameOfLesson);
        }
    }
}
