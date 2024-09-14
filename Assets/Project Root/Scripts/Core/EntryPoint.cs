using UnityEngine;
using LearningPrograms;
using Lessons;
using Localization;
using System;
using TMPro;
using UserInterface;

namespace Core
{
    public sealed class EntryPoint : MonoBehaviour
    {

        [Space, SerializeField]
        
        private LearningProgramView _programView;


        #region Localization

        [Space, SerializeField]

        private TMP_Dropdown _languageDropDown;

        #endregion


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


        [Space, SerializeField]

        private LessonDrawer _lessonDrawer;


        [Space, SerializeField]
        
        private LearningProgramPresenter _programPresenter;


        [Space, SerializeField]
        
        private LearningProgram _allLessons;


        [SerializeField]
        
        private TableOfLearningPrograms _tableOfLearningPrograms;


        [Space, SerializeField]
        
        private LessonEditor _lessonEditor;


        [SerializeField]
        
        private LearningProgramEditor _learningProgramEditor;



        private void Awake()
        {

            InitializeEducationSystem();

            InitializeUI();


            _programPresenter.SetTableOfPrograms(_tableOfLearningPrograms);

            _learningProgramEditor.DrawLessonToggles(_allLessons.Lessons);
        }


        private void OnEnable()
        {

            _lessonComponent.SetComponent();

            _programComponent.SetComponent();

            _educationSystem.SetSystem();


            _lessonComponent.LessonChanged += _lessonDrawer.RenderLesson;

            _programComponent.ProgramChanged += _programView.ViewLearningProgram;


            _uiPresenter.SetPresenter();
        }


        private void OnDisable()
        {

            _lessonComponent.UnsetComponent();

            _programComponent.UnsetComponent();

            _educationSystem.UnsetSystem();


            _lessonComponent.LessonChanged -= _lessonDrawer.RenderLesson;

            _programComponent.ProgramChanged -= _programView.ViewLearningProgram;


            _uiPresenter.UnsetPresenter();
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

            UpdateDropdown();
        }


        private void UpdateDropdown()
        {
            
            _languageDropDown.ClearOptions();


            foreach (Languages language in Enum.GetValues(typeof(Languages)))
            {

                TMP_Dropdown.OptionData option = new(language.ToString());

                _languageDropDown.options.Add(option);
            }

            _languageDropDown.RefreshShownValue();
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
        #endregion
    }
}