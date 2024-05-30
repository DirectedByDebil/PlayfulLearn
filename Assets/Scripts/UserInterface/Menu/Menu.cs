using UnityEngine;
using TMPro;
using Lessons;
using LearningPrograms;
using System;
using Localizations;
using UserInterface.Buttons;
using Initializations;

namespace UserInterface.Menu
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class Menu : MonoBehaviour, IInitialization
    {
        [SerializeField] private LessonDrawer _lessonDrawer;
        [SerializeField] private ButtonElementSettings _buttonElementSettings;
    
        [Space, SerializeField] private RectTransform _topPanel;
        [SerializeField] private TextMeshProUGUI _currentProgramNameText;
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private TMP_Dropdown _dropdown;

        [Space, SerializeField] private RectTransform _contentPosition;

        private LearningProgram _currentLearningProgram;
        private MenuPresenter _menuPresenter;
        private ButtonDrawer _buttonDrawer;

        [Space, SerializeField, Range(1, 6)] private int _lessonsInRow;
        [SerializeField, Range(0, 200)] private int _xPadding, _yPadding;

        public void Initialize()
        {
            InitializeMenuPresenter();

            _buttonDrawer = new ButtonDrawer(_buttonElementSettings, _contentPosition);

            SetLanguages();
        }
        private void InitializeMenuPresenter()
        {
            Vector2 padding = new(_buttonElementSettings.Size.x + _xPadding, _buttonElementSettings.Size.y + _yPadding),
                startPosition = new(_buttonElementSettings.Size.x - _xPadding, _topPanel.sizeDelta.y);

            MenuSettings settings = new(_lessonsInRow, padding, startPosition);
            _menuPresenter = new MenuPresenter(settings);
            _menuPresenter.OnButtonDraw += OnLessonButtonDrawn;
        }
        private void OnLessonButtonDrawn(Lesson lesson, Vector2 localPosition, out LessonButton button)
        {
            button = (LessonButton)_buttonDrawer.DrawButton(lesson, localPosition);

            //#TODO don't like but ok
            var temp = button;
            void action()
            {
                Languages language = (Languages)_dropdown.value;
                _lessonDrawer.RenderLesson(temp.Lesson, language);

                gameObject.SetActive(false);
            }

            button.Clicked += action;
        }

        private void SetLanguages()
        {
            _dropdown.ClearOptions();

            foreach(var language in Enum.GetValues(typeof(Languages)))
            {
                TMP_Dropdown.OptionData option = new (language.ToString());
                _dropdown.options.Add(option);
            }

            _dropdown.RefreshShownValue();
        }

        public void TryRedrawLearningProgram(LearningProgram learningProgram)
        {
            if (learningProgram != null && learningProgram != _currentLearningProgram)
            {
                _contentPosition.localPosition = new Vector3(0, 0);

                _currentLearningProgram = learningProgram;
                _currentProgramNameText.text = _currentLearningProgram.name;

                _progressBar.UpdateProgressBar(_currentLearningProgram);
                _menuPresenter.CheckAmountOfButtons(_currentLearningProgram.Lessons);
            }
        }

        public void RefreshLearningProgram(LearningProgram learningProgram)
        {
            if(learningProgram == _currentLearningProgram)
            {
                _progressBar.UpdateProgressBar(_currentLearningProgram);

                int index = _currentLearningProgram.Lessons.Count - 1;
                Lesson lesson = _currentLearningProgram.Lessons[index];
                _menuPresenter.DrawButton(index, lesson);
            }
        }
    }
}