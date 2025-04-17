using UINew;
using LearningPrograms;
using UnityEngine;

namespace Core
{
    public sealed class NewEntryPoint : MonoBehaviour
    {

        [SerializeField, Space]
        private MainMenuObserver _mainMenu;


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


            _mainMenu.ViewLearningPrograms(SessionData.AllLearningPrograms);

            _mainMenu.ViewLastLearningProgram(SessionData.LastLearningProgram);


            _progressSystem = new ProgressSystem();
        }


        public void Set()
        {

            _mainMenu.SetObserver();

            _mainMenu.LessonChanged += _progressSystem.OnLessonChanged;

            _mainMenu.LessonCompleting += _progressSystem.OnLessonCompleting;

            _mainMenu.LearningProgramChanged += OnLearningProgramChanged;


            _progressSystem.ProgressChanging += OnProgressChanging;
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
