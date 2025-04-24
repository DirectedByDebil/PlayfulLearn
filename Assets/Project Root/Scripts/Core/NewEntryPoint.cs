using UINew;
using Lessons;
using LearningPrograms;
using LessonsPractices;
using UnityEngine;

namespace Core
{
    public sealed class NewEntryPoint : MonoBehaviour
    {

        [SerializeField, Space]
        private MainMenuObserver _mainMenu;

        [SerializeField, Space]
        private UserAccountPage _userPage;

        [SerializeField, Space]
        private LessonPracticePage _lessonPracticePage;


        private LessonPracticeSystem _lessonPracticeSystem;


        private ProgressSystem _progressSystem;


        private void Awake()
        {

            Init();
        }


        private void OnEnable()
        {

            Set();
        }


        private void OnDestroy()
        {

            App.Save();
        }


        public void Init()
        {

            _mainMenu.Init();


            _userPage.Init();

            _mainMenu.SetUserPage(_userPage);


            _lessonPracticePage.Init();

            _mainMenu.SetLessonPracticePage(_lessonPracticePage);


            _mainMenu.ViewLearningPrograms(SessionData.AllLearningPrograms);

            _mainMenu.ViewLastLearningProgram(SessionData.LastLearningProgram);


            _userPage.ViewUser(SessionData.UserData);


            _lessonPracticeSystem = new LessonPracticeSystem(_lessonPracticePage);

            _progressSystem = new ProgressSystem();
        }


        public void Set()
        {

            _mainMenu.SetObserver();

            _mainMenu.LessonChanged += OnLessonChanged;

            _mainMenu.LearningProgramChanged += OnLearningProgramChanged;


            _progressSystem.ProgressChanging += OnProgressChanging;


            _lessonPracticeSystem.SetSytem();

            _lessonPracticePage.FinishClicked += OnLessonCompleting;
        }


        private void OnLessonChanged(Lesson lesson)
        {

            _progressSystem.OnLessonChanged(lesson);

            _lessonPracticePage.PreparePractice(lesson);
        }


        private void OnLessonCompleting()
        {

            _progressSystem.OnLessonCompleting();

            _mainMenu.OnLessonPracticeFinished();
        }


        private async void OnProgressChanging()
        {

            await _progressSystem.CountProgressAsync(SessionData.AllLearningPrograms);

            _mainMenu.ViewLearningPrograms(SessionData.AllLearningPrograms);

            _mainMenu.ViewLastLearningProgram(SessionData.LastLearningProgram);
        }


        private void OnLearningProgramChanged(LearningProgram program)
        {

            SessionData.SetLastLearningProgram(program);
        }
    }
}
