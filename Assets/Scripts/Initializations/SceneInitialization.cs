using UnityEngine;
using UserInterface.Menu;
using LearningPrograms;
using Lessons;

namespace Initializations
{
    public class SceneInitialization : MonoBehaviour
    {
        [SerializeField] private Menu _menu;
        [Space, SerializeField] private LearningProgramPresenter _programPresenter;
        [SerializeField] private LearningProgram _allLessons;
        [Space, SerializeField] private LessonEditor _lessonEditor;
        [SerializeField] private LearningProgramEditor _learningProgramEditor;
        [Space, SerializeField] private RectTransform _transparentPanel;

        private void Awake()
        {
            InitializeLessons();

            _menu.Initialize();
            _programPresenter.Initialize();
            _lessonEditor.Initialize();
            _learningProgramEditor.Initialize();

            _programPresenter.Switched += OnTransparentPanelSwitched;
            _lessonEditor.Switched += _transparentPanel.gameObject.SetActive;
            _lessonEditor.LessonAdded += ManageAddedLesson;
            _learningProgramEditor.Switched += OnTransparentPanelSwitched;
            
            _programPresenter.OnProgramPresented += _menu.TryRedrawLearningProgram;
            _programPresenter.LoadLastProgram();
        }

        private void InitializeLessons()
        {
            foreach (var lesson in _allLessons.Lessons)
            {
                lesson.Initialize();
            }
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
    }
}