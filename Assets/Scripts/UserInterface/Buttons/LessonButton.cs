using UnityEngine;
using UnityEngine.UI;
using Lessons;

namespace UserInterface.Buttons
{
    public sealed class LessonButton : ButtonElement
    {  
        public Lesson Lesson { get; private set; }

        public LessonButton(Lesson lesson, Vector2 size) : base (lesson.NameOfLesson, size)
        {
            UpdateLesson(lesson);
        }

        public void UpdateLesson(Lesson lesson)
        {
            Lesson = lesson;

            Image.sprite = Lesson.BackgroundSprite;
            Text.text = Lesson.NameOfLesson;
        }
        public void SetActive(bool isActive)
        {
            _obj.SetActive(isActive);
        }
    }
}