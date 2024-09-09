using UnityEngine;
using LearningPrograms;
using Lessons;
using Localization;
using System;
using System.Collections.Generic;
using TMPro;
using UserInterface;

namespace Core
{
    public sealed class EntryPoint : MonoBehaviour
    {

        [Space, SerializeField]
        
        private LearningProgramView _programView;


        #region Buttons

        [SerializeField, Space]

        private List<ExpandedButton> _lessonButtons;
        
        
        [SerializeField, Space]

        private List<ExpandedButton> _programButtons;

        #endregion


        #region Localization

        [Space, SerializeField]

        private TMP_Dropdown _languageDropDown;

        #endregion


        #region Education System

        private LessonComponent _lessonComponent;

        private LearningProgramComponent _programComponent;

        private EducationSystem _educationSystem;

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
        }


        private void OnDisable()
        {

            _lessonComponent.UnsetComponent();

            _programComponent.UnsetComponent();

            _educationSystem.UnsetSystem();


            _lessonComponent.LessonChanged -= _lessonDrawer.RenderLesson;

            _programComponent.ProgramChanged -= _programView.ViewLearningProgram;
        }


        private void Start()
        {

            _programComponent.LoadLearningPrograms(
                
                _tableOfLearningPrograms.LearningPrograms);


            _programComponent.SetLearningProgram(_allLessons);
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
                
                new LessonComponent(_lessonButtons);


            _programComponent =
                
                new LearningProgramComponent(_programButtons);


            _educationSystem = new EducationSystem(_lessonComponent,

                _programComponent);
        }

        private void InitializeUI()
        {


        }
        #endregion
    }
}