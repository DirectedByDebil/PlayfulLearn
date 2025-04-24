using UINew;

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

            _page.PracticeChanged += _model.SetPractice;

            _page.InputsChanged += _model.OnInputsChanged;

            _page.CheckClicked += _model.OnCheckingPractice;


            _model.InputChecked += _page.OnInputChecked;

            _model.PracticeChecked += _page.OnPracticeChecked;

            _model.MiniGameLoading += _sceneManager.LoadMiniGameAsync;


            _sceneManager.MiniGameFound += _model.OnMiniGameFound;
        }

    }
}
