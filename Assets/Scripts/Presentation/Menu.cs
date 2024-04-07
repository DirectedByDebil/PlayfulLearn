using UnityEngine;
using System.Collections.Generic;
using TMPro;
using LessonSystem;
using LearningProgramSystem;

namespace Presentation
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class Menu : MonoBehaviour
    {
        [SerializeField] private LessonDrawer _lessonDrawer;
        [SerializeField] private LearningProgramPresenter _learningProgramPresenter;
    
        [Space, SerializeField] private RectTransform _topPanel;
        [SerializeField] private TextMeshProUGUI _currentProgramNameText;

        [Space, SerializeField] private Transform _contentPosition;

        private RectTransform _rectTransform;

        private const int LESSONS_IN_ROW = 5,
            SPRITE_WIDTH = 300, SPRITE_HEIGHT = 300;
        
        private LearningProgram _currentLearningProgram;

        private List<LessonButton> _lessonsButtons = new ();
        private List<Vector3> _lessonButtonsPositions = new ();

        private float _xPadding;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();

            _xPadding = (_rectTransform.rect.width - SPRITE_WIDTH * LESSONS_IN_ROW) / (LESSONS_IN_ROW + 1);

            _learningProgramPresenter.OnProgramPresented += (LearningProgram learningProgram) =>
            { 
                TryRedrawLearningProgram(learningProgram);
            };

            TryRedrawLearningProgram(_learningProgramPresenter.LastChoosedProgram);
        }

        private void TryRedrawLearningProgram(LearningProgram learningProgram)
        {
            if (learningProgram != null && learningProgram != _currentLearningProgram)
            {
                _currentLearningProgram = learningProgram;

                _currentProgramNameText.text = _currentLearningProgram.name;

                _contentPosition.localPosition = new Vector3(0, _topPanel.sizeDelta.y);

                CheckAmountOfButtons();
            }
        }

        private void CheckAmountOfButtons()
        {
            int diff = _currentLearningProgram.Lessons.Count - _lessonsButtons.Count;

            if (diff <= 0)
            {
                LeanRedrawing();
            }
            else
                ExpensiveRedrawing();
        }
        private void LeanRedrawing()
        {
            int index = 0;

            foreach (var lesson in _currentLearningProgram.Lessons)
            {
                _lessonsButtons[index].SetActive(true);
                _lessonsButtons[index].UpdateLesson(lesson);

                index++;
            }

            for (int i = _lessonsButtons.Count - 1; i >= index; i--)
            {
                _lessonsButtons[i].SetActive(false);
            }
        }
        private void ExpensiveRedrawing()
        {
            int index = 0;

            foreach(var button in _lessonsButtons)
            {
                button.SetActive(true);
                button.UpdateLesson(_currentLearningProgram.Lessons[index]);
                
                index++;
            }

            AddButtonsPositions(_currentLearningProgram.Lessons.Count - index);

            for(int i = index; i < _currentLearningProgram.Lessons.Count; i++)
            {
                DrawLessonButton(_currentLearningProgram.Lessons[i], i);
            }
        }

        private void AddButtonsPositions(int amount)
        {
            int index = _lessonButtonsPositions.Count;

            for (int i = 0; i < amount; i++)
            {
                Vector3 newPosition = new Vector3(
                    _xPadding * (index % LESSONS_IN_ROW + 1) + (index % LESSONS_IN_ROW + 0.5f) * SPRITE_WIDTH,
                    -(index / LESSONS_IN_ROW + 0.5f) * SPRITE_HEIGHT - _topPanel.sizeDelta.y,
                    0);

                _lessonButtonsPositions.Add(newPosition);
                index++;
            }
        }
        private void DrawLessonButton(Lesson lesson, int index)
        {
            LessonButton lessonButton = new (lesson, new Vector2(SPRITE_WIDTH, SPRITE_HEIGHT));

            lessonButton.rectTransform.SetParent(_contentPosition, true);
            lessonButton.rectTransform.localPosition = _lessonButtonsPositions[index];
        
            lessonButton.onClickEvent += delegate
            {
                _lessonDrawer.CurrentLesson = lesson;
                _lessonDrawer.RenderLesson();

                gameObject.SetActive(false);
            };

            _lessonsButtons.Add(lessonButton);
        }
    }
}