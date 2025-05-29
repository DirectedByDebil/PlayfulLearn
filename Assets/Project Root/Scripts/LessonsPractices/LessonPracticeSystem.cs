using UINew;
using UnityEngine.UIElements;

namespace LessonsPractices
{
    public sealed class LessonPracticeSystem
    {

        private readonly LessonPracticePage _page;

        private readonly LessonPracticeModel _model;

        private readonly PracticeSceneManager _sceneManager;


        public LessonPracticeSystem(LessonPracticePage page)
        {

            _page = page;

            _model = new LessonPracticeModel();

            _sceneManager = new PracticeSceneManager();
        }


        public void SetSytem()
        {

            _page.PracticeChanged += OnPracticeChanged;

            _page.InputsChanged += _model.OnInputsChanged;

            _page.CheckClicked += _model.OnCheckingPractice;

            _page.FinishClicked += OnPracticeFinished;


            _model.InputChecked += _page.OnInputChecked;

            _model.PracticeChecked += _page.OnPracticeChecked;

            _model.MiniGameLoading += _sceneManager.LoadMiniGameAsync;

            _model.LockingInput += OnLockingInput;


            _sceneManager.MiniGameFound += _model.OnMiniGameFound;
        }


        private void OnPracticeChanged(LessonPractice practice)
        {

            OnPracticeFinished();

            _model.SetPractice(practice);
        }


        private void OnPracticeFinished()
        {

            _model.UnloadMiniGame();

            _sceneManager.UnLoadMiniGameAsync();
        }


        private void OnLockingInput(TextField field)
        {

            _page.SetFieldReadOnly(field, true);
        }
    }
}
