using UnityEngine;
using UnityEngine.UI;
using LessonSystem;

namespace Presentation
{
    public sealed class LessonButton : ButtonElement
    {   
        public LessonButton(Lesson lesson, Vector2 size) : base (lesson.NameOfLesson, size)
        {
            Image.sprite = lesson.BackgroundSprite;
            Text.text = lesson.NameOfLesson;
        }
    }
}