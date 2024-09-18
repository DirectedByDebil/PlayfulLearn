using UnityEngine;
using LearningPrograms;
using Lessons;
using System;
using UserInterface;

namespace Core
{
    public sealed class EntryPoint : MonoBehaviour
    {

        [Space, SerializeField]
        
        private LearningProgramView _programView;


        #region Education System

        private LessonComponent _lessonComponent;

        private LearningProgramComponent _programComponent;

        private EducationSystem _educationSystem;

        #endregion


        #region UI

        [SerializeField, Space]

        private UIView _uiView;

        private UIModel _uiModel;

        private UIPresenter _uiPresenter;

        #endregion


        #region Learning Program Editor

        [SerializeField, Space]

        private LearningProgramEditorView _lpEditorView;

        private LearningProgramEditorModel _lpEditorModel;

        private LearningProgramEditorPresenter _lpEditorPresenter;

        #endregion


        [Space, SerializeField]

        private LessonDrawer _lessonDrawer;


        [Space, SerializeField]
        
        private LearningProgram _allLessons;


        [SerializeField]
        
        private TableOfLearningPrograms _tableOfLearningPrograms;


        [Space, SerializeField]
        
        private LessonEditorView _lessonEditorView;



        private void Awake()
        {

            InitializeEducationSystem();

            InitializeUI();

            InitializeLearningProgramEditor();
        }


        private void OnEnable()
        {

            _lessonComponent.SetComponent();

            _programComponent.SetComponent();

            _educationSystem.SetSystem();


            _lessonComponent.LessonChanged += _lessonDrawer.RenderLesson;

            _programComponent.ProgramChanged += _programView.ViewLearningProgram;


            _uiPresenter.SetPresenter();

            _uiPresenter.SetEditor(_lessonEditorView);

            _uiPresenter.SetEditor(_lpEditorView);


            _lpEditorModel.SetModel();

            _lpEditorPresenter.SetPresenter();
        }


        private void OnDisable()
        {

            _lessonComponent.UnsetComponent();

            _programComponent.UnsetComponent();

            _educationSystem.UnsetSystem();


            _lessonComponent.LessonChanged -= _lessonDrawer.RenderLesson;

            _programComponent.ProgramChanged -= _programView.ViewLearningProgram;


            _uiPresenter.UnsetPresenter();

            _uiPresenter.UnsetEditor(_lessonEditorView);

            _uiPresenter.UnsetEditor(_lpEditorView);


            _lpEditorModel.UnsetModel();

            _lpEditorPresenter.UnsetPresenter();
        }


        private void Start()
        {

            _programComponent.LoadLearningPrograms(
                
                _tableOfLearningPrograms.LearningPrograms);


            _programComponent.SetLearningProgram(_allLessons);


            _uiModel.ChangeState(UIStates.CurrentLearningProgram);
        }


        private void OnValidate()
        {
            
            _lpEditorView.SetAllLessons(_allLessons.Lessons);
        }


        #region Program Initialization

        private void InitializeEducationSystem()
        {

            _lessonComponent =
                
                new LessonComponent(_uiView.LessonButtons);


            _programComponent =
                
                new LearningProgramComponent(_uiView.ProgramButtons);


            _educationSystem = new EducationSystem(_lessonComponent,

                _programComponent);
        }


        private void InitializeUI()
        {

            UIStates[] states = (UIStates[])
                
                Enum.GetValues(typeof(UIStates));


            _uiModel = new UIModel(states);


            _uiModel.LoadObjects(_uiView.UIObjects);


            _uiPresenter = new UIPresenter(_uiModel, _uiView);
        }


        private void InitializeLearningProgramEditor()
        {

            _lpEditorModel = new LearningProgramEditorModel(

                _lpEditorView.Toggles);


            _lpEditorPresenter = new LearningProgramEditorPresenter(

                _lpEditorModel, _lpEditorView);


            _lpEditorModel.InitializeToggles(_allLessons.Lessons);
        }

        #endregion
    }
}