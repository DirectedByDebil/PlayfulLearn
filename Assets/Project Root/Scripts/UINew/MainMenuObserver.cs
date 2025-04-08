using Core;
using Lessons;
using LearningPrograms;
using UnityEngine;
using System.Collections.Generic;

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


        private void Start()
        {

            Init();


            List<Lesson> lessons = new(SessionData.AllLessons);

            _currentProgramPage.ViewLessons(lessons);


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


            _allProgramsPage.Hide();

            _userAccountPage.Hide();
        }


        public void SetObserver()
        {

            _currentProgramPage.OpenLearningProgramsClicked +=
                _allProgramsPage.Show;


            _currentProgramPage.UserAccountClicked +=
                _userAccountPage.Show;


            _userAccountPage.CloseClicked += _userAccountPage.Hide;

            _allProgramsPage.CloseClicked += _allProgramsPage.Hide;
        }


    }
}
