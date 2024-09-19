using Localization;

namespace Lessons
{
    public sealed class LessonEditorPresenter
    {

        private readonly LessonEditorModel _model;

        private readonly LessonEditorView _view;


        private Languages _lastLanguage;


        public LessonEditorPresenter(LessonEditorModel model,
            
            LessonEditorView view)
        {

            _model = model;

            _view = view;
        }


        public void SetPresenter()
        {

            _view.LanguageChanged.AddListener(

                OnLanguageChanged);


            _view.CreateClicked.AddListener(

                OnCreateClicked);
        }


        public void UnsetPresenter()
        {

            _view.LanguageChanged.RemoveListener(

                OnLanguageChanged);


            _view.CreateClicked.RemoveListener(

                OnCreateClicked);
        }


        private void OnLanguageChanged(int value)
        {
           
            _model.UpdateContent(_lastLanguage,

                _view.GetTextContent());


            _view.SetTextContent(

                _model.GetContent(_view.CurrentLanguage));


            _lastLanguage = _view.CurrentLanguage;
        }


        private void OnCreateClicked()
        {

            _model.UpdateContent(_view.CurrentLanguage,

                _view.GetTextContent());


            _model.SaveLesson(_view.NameOfLesson);
        }
    }
}
