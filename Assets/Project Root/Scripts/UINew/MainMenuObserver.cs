using Core;
using Lessons;
using LearningPrograms;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace UINew
{

    public sealed class MainMenuObserver : MonoBehaviour
    {

        public event Action<LearningProgram> LearningProgramChanged;

        public event Action<Lesson> LessonChanged;


        [SerializeField, Space]
        private CurrentLearningProgramPage _currentProgramPage;


        [SerializeField, Space]
        private AllLearningProgramsPage _allProgramsPage;


        [SerializeField, Space]
        private LessonPage _lessonPage;

        
        private IPage _userPage;

        private IPage _lessonPracticePagew;


        private IPage _currentPage;
        

        public void Init()
        {

            _currentProgramPage.Init();

            _allProgramsPage.Init();

            _lessonPage.Init();



            _allProgramsPage.Hide();

            _lessonPage.Hide();


            ShowCurrentLearningProgram();
        }


        #region Set Pages

        public void SetUserPage(IPage userPage)
        {

            _userPage = userPage;

            _userPage.Hide();
        }


        public void SetLessonPracticePage(IPage lessonPracticePage)
        {

            _lessonPracticePagew = lessonPracticePage;

            _lessonPracticePagew.Hide();
        }

        #endregion


        public void SetObserver()
        {

            _currentProgramPage.OpenLearningProgramsClicked +=
                _allProgramsPage.Show;


            _currentProgramPage.UserAccountClicked +=
                OnUserAccountClicked;


            _currentProgramPage.LessonClicked += OnLessonClicked;


            _allProgramsPage.Closing += _allProgramsPage.Hide;


            _allProgramsPage.LearningProgramClicked += OnLearningProgramClicked;


            _lessonPage.Closing += OnLessonBackClicked;

            _lessonPage.StartClicked += OnStartPracticeClicked;


            _userPage.Closing += ShowCurrentLearningProgram;
        }


        #region View Learning Programs Methods 

        public void ViewLastLearningProgram(LearningProgram last)
        {

            _currentProgramPage.ViewLearningProgram(last);
        }


        public void ViewLearningPrograms(IList<LearningProgram> allPrograms)
        {

            _allProgramsPage.ViewLearningPrograms(allPrograms);
        }

        #endregion


        public void OnLessonPracticeFinished()
        {

            ChangePage(_currentProgramPage);
        }


        private void ShowCurrentLearningProgram()
        {

            ChangePage(_currentProgramPage);

            _currentProgramPage.ViewUserIcon(SessionData.SelectedCharacter.Icon);
        }


        #region On Buttons Clicked

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


            LessonChanged?.Invoke(lesson);
        }


        private void OnUserAccountClicked()
        {

            ChangePage(_userPage);
        }


        private void OnLessonBackClicked()
        {

            ChangePage(_currentProgramPage);
        }


        private void OnStartPracticeClicked()
        {

            ChangePage(_lessonPracticePagew);
        }

        #endregion


        private void ChangePage(IPage newPage)
        {

            _currentPage?.Hide();


            _currentPage = newPage;

            _currentPage.Show();
        }
    }
}
