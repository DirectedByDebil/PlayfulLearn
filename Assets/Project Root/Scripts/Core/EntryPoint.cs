using UnityEngine;
using LearningPrograms;
using Lessons;
using System;
using UserInterface;
using Localization;
using System.Collections.Generic;
using Extensions;
using Users;

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


        [SerializeField, Space]

        private UserView _userView;

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

        private List<ExpandedButton> _lessonButtons;

        private List<ExpandedButton> _learningProgramsButtons;
        
        private List<ExpandedToggle> _lessonEditorToggles;

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

            _uiPresenter.SetUserAccount(_userView);


            _lessonEditorModel.LessonCreated += OnLessonCreated;

            _lessonEditorPresenter.SetPresenter();


            _lpEditorModel.SetModel();

            _lpEditorModel.LearningProgramCreated += OnLearningProgramCreated;

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

            _uiPresenter.UnsetUserAccount(_userView);


            _lessonEditorModel.LessonCreated -= OnLessonCreated;

            _lessonEditorPresenter.UnsetPresenter();


            _lpEditorModel.UnsetModel();

            _lpEditorModel.LearningProgramCreated -= OnLearningProgramCreated;

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

            List<string> GetLessonsNames(IReadOnlyCollection<Lesson> _lessons)
            {

                List<string> lessonsNames = new(_lessons.Count);


                foreach (Lesson lesson in _lessons)
                {

                    lessonsNames.Add(lesson.NameOfLesson);
                }


                return lessonsNames;
            }


            string name = _programComponent.GetCurrentProgramName();


            FileExtensions.WriteFile(name, PathKeeper.LastLearningProgramName);



            LearningProgramData data = new("All Lessons",
                
                "Unity.png", GetLessonsNames(SessionData.AllLessons));


            string fileName = string.Format(

                "{0}/{1}.json", PathKeeper.LearningProgramsPath,

                "All Lessons");


            FileExtensions.WriteJson(data, fileName);
        }


        private void OnLessonCreated(LessonData data)
        {

            Lesson lesson = new (data);

            lesson.InitializeContent();

            lesson.LoadIcon();


            SessionData.AddLesson(lesson);


            if(_lessonsGrid.TryUpdateCount(SessionData.AllLessons.Count))
            {
                
                ExpandedButton button = 
                    
                    _lessonsGrid.GetObject<ExpandedButton>(SessionData.AllLessons.Count-1);


                _lessonComponent.AddButton(button, lesson);

                _lessonButtons.Add(button);

                _uiPresenter.SetLessonButton(button);
            }


            

            bool isAdded = _lessonsTogglesGrid.TryUpdateCount(
                
                SessionData.AllLessons.Count);

            
            ExpandedToggle toggle = 
                
                _lessonsTogglesGrid.GetObject<ExpandedToggle>(SessionData.AllLessons.Count-1);


            toggle.gameObject.SetActive(true);

            toggle.UpdateText(lesson.NameOfLesson);


            if(isAdded)
            {

                _lessonEditorToggles.Add(toggle);

                _lpEditorModel.AddToggle(toggle, lesson);
            }
        }


        private async void OnLearningProgramCreated(LearningProgramData data)
        {

            LearningProgram program = new (data);


            await program.AddLessonsAsync(SessionData.AllLessons);

            program.LoadIcon();


            SessionData.AddLearningProgram(program);


            bool isAdded = _learningProgramsGrid.TryUpdateCount(SessionData.AllLearningPrograms.Count);


            ExpandedButton button = _learningProgramsGrid.GetObject<
                
                ExpandedButton>(SessionData.AllLearningPrograms.Count-1);

            button.gameObject.SetActive(true);


            if(isAdded)
            {

                _learningProgramsButtons.Add(button);

                _programComponent.AddButton(button, program);


                _uiPresenter.SetProgramButton(button);
            }
            else
            {
                //#TODO watch AddButton
                button.UpdateIcon(program.Icon);

                button.UpdateText(program.NameOfProgram);
            }
        }


        #region Program Initialization

        private void InitializeGrids()
        {

            _lessonsGrid.TryUpdateCount(SessionData.AllLessons.Count);


            _lessonButtons = new List<ExpandedButton>(
                
                _lessonsGrid.GetObjects<ExpandedButton>());


            _learningProgramsGrid.TryUpdateCount(
                
                SessionData.AllLearningPrograms.Count);


            _learningProgramsButtons = new List<ExpandedButton>(

                _learningProgramsGrid.GetObjects<ExpandedButton>());


            _lessonsTogglesGrid.TryUpdateCount(SessionData.AllLessons.Count);

            _lessonsTogglesGrid.ShowOnly(SessionData.AllLessons.Count);


            _lessonEditorToggles = new List<ExpandedToggle>(

                _lessonsTogglesGrid.GetObjects<ExpandedToggle>());
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