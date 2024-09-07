﻿using UnityEngine;
using UserInterface.Menu;
using LearningPrograms;
using Lessons;

namespace Core
{
    public sealed class EntryPoint : MonoBehaviour
    {

        [SerializeField] private Menu _menu;

        [SerializeField] private LearningProgramPresenter _programPresenter;


        [Space, SerializeField] private LearningProgram _allLessons;

        [SerializeField] private TableOfLearningPrograms _tableOfLearningPrograms;


        [Space, SerializeField] private LessonEditor _lessonEditor;

        [SerializeField] private LearningProgramEditor _learningProgramEditor;


        [Space, SerializeField] private RectTransform _transparentPanel;


        private void Awake()
        {

            _menu.Initialize();
            _programPresenter.Initialize();
            _lessonEditor.Initialize();
            _learningProgramEditor.Initialize();


            _programPresenter.SetTableOfPrograms(_tableOfLearningPrograms);
            _learningProgramEditor.DrawLessonToggles(_allLessons.Lessons);
        }


        private void OnEnable()
        {
        
            _programPresenter.Switched += OnTransparentPanelSwitched;

            _lessonEditor.Switched += _transparentPanel.gameObject.SetActive;

            _lessonEditor.LessonAdded += ManageAddedLesson;

            _learningProgramEditor.LearningProgramCreated += OnLearningProgramCreated;

            _learningProgramEditor.Switched += OnTransparentPanelSwitched;
            

            _programPresenter.OnProgramPresented += _menu.TryRedrawLearningProgram;            
        }


        private void OnDisable()
        {

            _programPresenter.Switched -= OnTransparentPanelSwitched;

            _lessonEditor.Switched -= _transparentPanel.gameObject.SetActive;

            _lessonEditor.LessonAdded -= ManageAddedLesson;

            _learningProgramEditor.LearningProgramCreated -= OnLearningProgramCreated;

            _learningProgramEditor.Switched -= OnTransparentPanelSwitched;


            _programPresenter.OnProgramPresented -= _menu.TryRedrawLearningProgram;
        }


        private void OnTransparentPanelSwitched(bool value)
        {

            if(!_learningProgramEditor.gameObject.activeInHierarchy)
            {

                _transparentPanel.gameObject.SetActive(value);
            }
        }


        private void ManageAddedLesson(Lesson lesson)
        {
            _allLessons.Lessons.Add(lesson);
            _menu.RefreshLearningProgram(_allLessons);
        }

        private void OnLearningProgramCreated(LearningProgram learningProgram)
        {
            _tableOfLearningPrograms.LearningPrograms.Add(learningProgram);
            _programPresenter.RefreshTableOfPrograms(_tableOfLearningPrograms);
        }
    }
}