using System.Collections.Generic;

namespace UserInterface
{
    public sealed class UIPresenter
    {

        private readonly UIModel _model;

        private readonly UIView _view;


        public UIPresenter(UIModel model,
            
            UIView view)
        {

            _model = model;

            _view = view;
        }



        public void SetPresenter()
        {

            _view.AvailableProgramsClicked.AddListener(

                StateAvailablePrograms);


            _view.BackToProgramClicked.AddListener(
                
                StateCurrentProgram);



            _view.OpenLessonEditorClicked.AddListener(

                StateLessonEditor);


            _view.OpenProgramEditorClicked.AddListener(

                StateProgramEditor);
        }


        public void UnsetPresenter()
        {

            _view.AvailableProgramsClicked.RemoveListener(
                
                StateAvailablePrograms);


            _view.BackToProgramClicked.RemoveListener(

                StateCurrentProgram);



            _view.OpenLessonEditorClicked.RemoveListener(

                StateLessonEditor);


            _view.OpenProgramEditorClicked.RemoveListener(

                StateProgramEditor);
        }


        public void SetLessonButtons(IEnumerable<IClickable> buttons)
        {
            
            foreach (IClickable button in buttons)
            {
                
                button.onClick.AddListener(StateCurrentLesson);
            }
        }


        public void UnsetLessonButtons(IEnumerable<IClickable> buttons)
        {

            foreach (IClickable button in buttons)
            {

                button.onClick.RemoveListener(StateCurrentLesson);
            }
        }


        public void SetProgramButtons(IEnumerable<IClickable> buttons)
        {

            foreach (IClickable button in buttons)
            {

                button.onClick.AddListener(StateCurrentProgram);
            }
        }


        public void UnsetProgramButtons(IEnumerable<IClickable> buttons)
        {

            foreach (IClickable button in buttons)
            {

                button.onClick.RemoveListener(StateCurrentProgram);
            }
        }


        public void SetEditor(IEditor editor)
        {

            editor.CreateClicked.AddListener(

                StateCurrentProgram);


            editor.CloseClicked.AddListener(

                StateCurrentProgram);
        }


        public void UnsetEditor(IEditor editor)
        {

            editor.CreateClicked.RemoveListener(

                StateCurrentProgram);


            editor.CloseClicked.RemoveListener(

                StateCurrentProgram);
        }


        #region State UIState Methods

        private void StateCurrentLesson()
        {

            _model.ChangeState(UIStates.CurrentLesson);
        }


        private void StateCurrentProgram()
        {

            _model.ChangeState(UIStates.CurrentLearningProgram);
        }


        private void StateAvailablePrograms()
        {

            _model.ChangeState(UIStates.AvailableLearningPrograms);
        }


        private void StateLessonEditor()
        {

            _model.ChangeState(UIStates.LessonEditor);
        }


        private void StateProgramEditor()
        {

            _model.ChangeState(UIStates.ProgramEditor);
        }
        
        #endregion
    }
}
