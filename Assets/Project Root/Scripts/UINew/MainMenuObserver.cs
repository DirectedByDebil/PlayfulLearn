using Lessons;
using LearningPrograms;
using Web;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace UINew
{
    public sealed class MainMenuObserver : MonoBehaviour
    {

        public event Action<LearningProgram> LearningProgramChanged;

        public event Action<Lesson> LessonChanged;

        public event Action LessonCompleting;


        [SerializeField, Space]
        private CurrentLearningProgramPage _currentProgramPage;


        [SerializeField, Space]
        private AllLearningProgramsPage _allProgramsPage;


        [SerializeField, Space]
        private UserAccountPage _userAccountPage;

        [SerializeField, Space]
        private LessonPage _lessonPage;

        [SerializeField, Space]
        private LessonPracticePage _lessonPracticePage;


        private IPage _currentPage;
        

        public void Init()
        {

            _currentProgramPage.Init();

            _allProgramsPage.Init();

            _userAccountPage.Init();

            _lessonPage.Init();

            _lessonPracticePage.Init();


            _allProgramsPage.Hide();

            _userAccountPage.Hide();

            _lessonPage.Hide();

            _lessonPracticePage.Hide();


            _currentPage = _currentProgramPage;
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


            _lessonPracticePage.FinishClicked += OnFinishedClicked;
        }


        public void ViewUser(UserData user)
        {

            _userAccountPage.ViewUser(user);
        }



        public void ViewLastLearningProgram(LearningProgram last)
        {

            _currentProgramPage.ViewLearningProgram(last);
        }


        public void ViewLearningPrograms(IList<LearningProgram> allPrograms)
        {

            _allProgramsPage.ViewLearningPrograms(allPrograms);
        }



        private void OnLearningProgramClicked(LearningProgram program)
        {

            _currentProgramPage.ViewLearningProgram(program);

            _allProgramsPage.Hide();


            LearningProgramChanged?.Invoke(program);
        }


        private void OnLessonClicked(Lesson lesson)
        {

            ChangePage(_lessonPage);

            _lessonPage.ViewLesson(lesson);


            _lessonPracticePage.PreparePractice(lesson);


            LessonChanged?.Invoke(lesson);
        }


        private void OnLessonBackClicked()
        {

            ChangePage(_currentProgramPage);
        }


        private void OnStartPracticeClicked()
        {

            ChangePage(_lessonPracticePage);
        }


        private void OnFinishedClicked()
        {

            ChangePage(_currentProgramPage);

            LessonCompleting?.Invoke();
        }


        private void ChangePage(IPage newPage)
        {

            _currentPage.Hide();


            _currentPage = newPage;

            _currentPage.Show();
        }
    }
}
