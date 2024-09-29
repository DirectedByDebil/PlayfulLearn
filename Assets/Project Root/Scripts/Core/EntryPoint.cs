using UnityEngine;
using LearningPrograms;
using Lessons;
using System;
using UserInterface;
using Localization;
using System.Collections.Generic;
using Extensions;

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


        #region Lesson Editor

        [Space, SerializeField]
        
        private LessonEditorView _lessonEditorView;

        private LessonEditorModel _lessonEditorModel;

        private LessonEditorPresenter _lessonEditorPresenter;
        
        #endregion


        #region Learning Program Editor

        [SerializeField, Space]

        private LearningProgramEditorView _lpEditorView;

        private LearningProgramEditorModel _lpEditorModel;

        private LearningProgramEditorPresenter _lpEditorPresenter;

        #endregion


        #region Grids

        [SerializeField, Space]

        private ObjectGrid _lessonsGrid;


        [SerializeField, Space]

        private ObjectGrid _learningProgramsGrid;


        [SerializeField, Space]

        private ObjectGrid _lessonsTogglesGrid;

        #endregion


        #region Buttons & Toggles

        private IReadOnlyList<ExpandedButton> _lessonButtons;

        private IReadOnlyList<ExpandedButton> _learningProgramsButtons;
        
        private IReadOnlyList<ExpandedToggle> _lessonEditorToggles;

        #endregion


        [Space, SerializeField]

        private LessonDrawer _lessonDrawer;


        private Languages[] _languages;



        private void Awake()
        {

            InitializeGrids();

            InitializeEducationSystem();

            InitializeUI();

            InitializeLessonEditor();

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

            _uiPresenter.SetLessonButtons(_lessonButtons);

            _uiPresenter.SetProgramButtons(_learningProgramsButtons);


            _lessonEditorPresenter.SetPresenter();


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

            _uiPresenter.UnsetLessonButtons(_lessonButtons);

            _uiPresenter.UnsetProgramButtons(_learningProgramsButtons);


            _lessonEditorPresenter.UnsetPresenter();


            _lpEditorModel.UnsetModel();

            _lpEditorPresenter.UnsetPresenter();
        }


        private void Start()
        {
            
            _programComponent.LoadLearningPrograms(
                
                SessionData.AllLearningPrograms);


            _lpEditorView.SetAllLessons(SessionData.AllLessons,
                
                _lessonEditorToggles);


            _programComponent.SetLearningProgram(
                
                SessionData.LastLearningProgram);


            _uiModel.ChangeState(UIStates.CurrentLearningProgram);
        }


        private void OnValidate()
        {

            _languages = (Languages[])

                Enum.GetValues(typeof(Languages));


            _lessonEditorView.SetLanguages(_languages);
        }


        private void OnDestroy()
        {

            string name = _programComponent.GetCurrentProgramName();


            FileExtensions.WriteFile(name, PathKeeper.LastLearningProgramName);
        }


        #region Program Initialization

        private void InitializeGrids()
        {

            _lessonsGrid.SetCount(SessionData.AllLessons.Count);


            _lessonButtons =
                
                _lessonsGrid.GetObjects<ExpandedButton>();


            _learningProgramsGrid.SetCount(
                
                SessionData.AllLearningPrograms.Count);


            _learningProgramsButtons =

                _learningProgramsGrid.GetObjects<ExpandedButton>();


            _lessonsTogglesGrid.SetCount(SessionData.AllLessons.Count);


            _lessonEditorToggles =

                _lessonsTogglesGrid.GetObjects<ExpandedToggle>();
        }


        private void InitializeEducationSystem()
        {

            _lessonComponent =
                
                new LessonComponent(_lessonButtons);


            _programComponent =
                
                new LearningProgramComponent(_learningProgramsButtons);


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


        private void InitializeLessonEditor()
        {

            _lessonEditorModel = new LessonEditorModel(_languages);


            _lessonEditorPresenter = new LessonEditorPresenter(
                
                _lessonEditorModel, _lessonEditorView);
        }


        private void InitializeLearningProgramEditor()
        {

            _lpEditorModel = new LearningProgramEditorModel(_lessonEditorToggles);


            _lpEditorPresenter = new LearningProgramEditorPresenter(

                _lpEditorModel, _lpEditorView);


            _lpEditorModel.InitializeToggles(SessionData.AllLessons);
        }

        #endregion
    }
}