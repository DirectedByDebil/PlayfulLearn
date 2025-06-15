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


        public void SetSystem()
        {

            _page.PracticeChanged += OnPracticeChanged;

            _page.InputsChanged += _model.OnInputsChanged;

            _page.CheckClicked += _model.OnCheckingPractice;

            _page.FinishClicked += OnPracticeFinished;

            _page.Closing += OnPracticeFinished;


            _model.InputChecked += _page.OnInputChecked;

            _model.PracticeChecked += _page.OnPracticeChecked;

            _model.PracticeLoading += _sceneManager.LoadPracticeAsync;

            _model.LockingInput += OnLockingInput;


            _sceneManager.MiniGameFound += _model.OnMiniGameFound;

            _sceneManager.BlocksPracticeFound += _model.OnBlocksPracticeFound;
        }


        private void OnPracticeChanged(LessonPractice practice)
        {

            OnPracticeFinished();

            _model.SetPractice(practice);
        }


        private void OnPracticeFinished()
        {

            _model.UnloadMiniGame();

            _model.UnloadBlocks();

            _sceneManager.UnLoadPracticeAsync();
        }


        private void OnLockingInput(TextField field)
        {

            _page.SetFieldReadOnly(field, true);
        }
    }
}
