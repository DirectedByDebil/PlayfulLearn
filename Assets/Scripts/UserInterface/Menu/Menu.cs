using UnityEngine;
using TMPro;
using Lessons;
using LearningPrograms;
using System;
using Localizations;
using UserInterface.Buttons;

namespace UserInterface.Menu
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class Menu : MonoBehaviour
    {
        [SerializeField] private LessonDrawer _lessonDrawer;
        [SerializeField] private LearningProgramPresenter _learningProgramPresenter;
    
        [Space, SerializeField] private RectTransform _topPanel;
        [SerializeField] private TextMeshProUGUI _currentProgramNameText;
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private TMP_Dropdown _dropdown;

        [Space, SerializeField] private RectTransform _contentPosition;

        private const int SPRITE_WIDTH = 300, SPRITE_HEIGHT = 300;
        
        private LearningProgram _currentLearningProgram;
        private MenuPresenter _menuPresenter;
        private ButtonDrawer _buttonDrawer;

        [Space, SerializeField, Range(1, 6)] private int _lessonsInRow;
        [SerializeField, Range(0, 200)] private int _xPadding;

        private void Start()
        {
            InitializeMenuPresenter();
            
            Vector2 slotSize = new(SPRITE_WIDTH, SPRITE_HEIGHT);
            _buttonDrawer = new ButtonDrawer(slotSize, _contentPosition);

            SetLanguages();

            _learningProgramPresenter.OnProgramPresented += TryRedrawLearningProgram;
            _learningProgramPresenter.Initialize();
        }
        private void InitializeMenuPresenter()
        {
            Vector2 padding = new(SPRITE_WIDTH + _xPadding, SPRITE_HEIGHT),
                startPosition = new(SPRITE_WIDTH - _xPadding, _topPanel.sizeDelta.y);

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

        private void TryRedrawLearningProgram(LearningProgram learningProgram)
        {
            if (learningProgram != null && learningProgram != _currentLearningProgram)
            {
                _currentLearningProgram = learningProgram;

                _currentProgramNameText.text = _currentLearningProgram.name;

                _contentPosition.localPosition = new Vector3(0, 0);

                _progressBar.UpdateProgressBar(learningProgram);

                _menuPresenter.CheckAmountOfButtons(_currentLearningProgram.Lessons);
            }
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
    }
}