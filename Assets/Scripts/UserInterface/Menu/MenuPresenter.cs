using Lessons;
using UnityEngine;
using System.Collections.Generic;
using UserInterface.Buttons;

namespace UserInterface.Menu
{
    public class MenuPresenter
    {
        public delegate void DrawButtonHandler(Lesson lesson, Vector2 localPosition, out LessonButton button);
        public event DrawButtonHandler OnButtonDraw;

        private readonly MenuSettings _settings;

        private List<LessonButton> _lessonsButtons = new();
        private List<Vector3> _lessonButtonsPositions  = new();

        public MenuPresenter(MenuSettings settings)
        {
            _settings = settings;
        }

        public void CheckAmountOfButtons(List<Lesson> lessons)
        {
            int diff = lessons.Count - _lessonsButtons.Count;

            if (diff <= 0)
            {
                LeanRedrawing(lessons);
            }
            else
                ExpensiveRedrawing(lessons);
        }
        private void LeanRedrawing(List<Lesson> lessons)
        {
            int index = 0;

            foreach (var lesson in lessons)
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
        private void ExpensiveRedrawing(List<Lesson> lessons)
        {
            int index = 0;

            foreach (var button in _lessonsButtons)
            {
                button.SetActive(true);
                button.UpdateLesson(lessons[index]);

                index++;
            }

            AddButtonsPositions(lessons.Count - index);

            for (int i = index; i < lessons.Count; i++)
            {
                Lesson lesson = lessons[i];
                Vector2 localPosition = _lessonButtonsPositions[i];
                LessonButton button = null;

                OnButtonDraw?.Invoke(lesson, localPosition, out button);
                _lessonsButtons.Add(button);
            }
        }
        private void AddButtonsPositions(int amount)
        {
            int index = _lessonButtonsPositions.Count;

            for (int i = 0; i < amount; i++)
            {
                Vector3 newPosition = new (
                    (index % _settings.ObjectsInRow) * _settings.Padding.x + _settings._startPosition.x,
                    -(index / _settings.ObjectsInRow) * _settings.Padding.y - _settings._startPosition.y,
                    0);

                _lessonButtonsPositions.Add(newPosition);
                index++;
            }
        }
    }
}