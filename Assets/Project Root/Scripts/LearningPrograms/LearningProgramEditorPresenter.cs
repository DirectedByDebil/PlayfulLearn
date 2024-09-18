namespace LearningPrograms
{
    public sealed class LearningProgramEditorPresenter
    {

        private readonly LearningProgramEditorModel _model;

        private readonly LearningProgramEditorView _view;


        public LearningProgramEditorPresenter(
            
            LearningProgramEditorModel model,
            
            LearningProgramEditorView view)
        {

            _model = model;

            _view = view;
        }


        public void SetPresenter()
        {

            _view.CreateClicked.AddListener(

                OnCreateClicked);
        }


        public void UnsetPresenter()
        {

            _view.CreateClicked.RemoveListener(

                OnCreateClicked);
        }


        private void OnCreateClicked()
        {

            _model.CreateProgram(_view.NameOfProgram);
        }
    }
}
