using UnityEngine.UI;

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


            _view.CreateLessonClicked.AddListener(

                StateCurrentProgram);


            _view.CloseLessonEditorClicked.AddListener(

                StateCurrentProgram);



            _view.OpenProgramEditorClicked.AddListener(

                StateProgramEditor);


            _view.CreateProgramClicked.AddListener(

                StateCurrentProgram);


            _view.CloseProgramEditorClicked.AddListener(

                StateCurrentProgram);



            foreach (Button.ButtonClickedEvent clicked in
                
                _view.LessonsClicked)
            {

                clicked.AddListener(StateCurrentLesson);
            }


            foreach (Button.ButtonClickedEvent clicked in

                _view.ProgramsClicked)
            {

                clicked.AddListener(StateCurrentProgram);
            }
        }


        public void UnsetPresenter()
        {

            _view.AvailableProgramsClicked.RemoveListener(
                
                StateAvailablePrograms);


            _view.BackToProgramClicked.RemoveListener(

                StateCurrentProgram);



            _view.OpenLessonEditorClicked.RemoveListener(

                StateLessonEditor);


            _view.CreateLessonClicked.RemoveListener(

                StateCurrentProgram);


            _view.CloseLessonEditorClicked.RemoveListener(

                StateCurrentProgram);



            _view.OpenProgramEditorClicked.RemoveListener(

                StateProgramEditor);


            _view.CreateProgramClicked.RemoveListener(

                StateCurrentProgram);


            _view.CloseProgramEditorClicked.RemoveListener(

                StateCurrentProgram);



            foreach (Button.ButtonClickedEvent clicked in
                
                _view.LessonsClicked)
            {

                clicked.RemoveListener(StateCurrentLesson);
            }


            foreach (Button.ButtonClickedEvent clicked in

                _view.ProgramsClicked)
            {

                clicked.RemoveListener(StateCurrentProgram);
            }
        }


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
    }
}
