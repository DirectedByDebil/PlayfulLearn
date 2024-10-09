using SimpleFileBrowser;

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


            _view.LoadIconClicked.AddListener(

                OnLoadIconClicked);
        }


        public void UnsetPresenter()
        {

            _view.CreateClicked.RemoveListener(

                OnCreateClicked);


            _view.LoadIconClicked.RemoveListener(

                 OnLoadIconClicked);
        }


        private void OnCreateClicked()
        {

            _model.CreateProgram(_view.NameOfProgram);
        }


        private void OnLoadIconClicked()
        {

            FileBrowser.ShowLoadDialog(OnSuccess, null,
                
                FileBrowser.PickMode.Files);
        }


        private void OnSuccess(string[] paths)
        {

            _model.SetIconPath(paths[0]);

            _view.SetIconPath(paths[0]);
        }
    }
}
