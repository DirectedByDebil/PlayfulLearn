using UnityEngine;
using UnityEngine.UI;
using LessonSystem;

namespace Presentation
{
    public sealed class LessonButton : ButtonElement
    {   
        public LessonButton(Lesson lesson, Vector2 size) : base (lesson.NameOfLesson, size)
        {
            UpdateLesson(lesson);
        }

        public void UpdateLesson(Lesson lesson)
        {
            Image.sprite = lesson.BackgroundSprite;
            Text.text = lesson.NameOfLesson;
        }
        public void SetActive(bool isActive)
        {
            _obj.SetActive(isActive);
        }
    }
}