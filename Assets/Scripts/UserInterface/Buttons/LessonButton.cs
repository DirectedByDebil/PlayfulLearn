using Lessons;

namespace UserInterface.Buttons
{
    public sealed class LessonButton : ButtonElement
    {  
        public Lesson Lesson { get; private set; }

        public LessonButton(Lesson lesson, ButtonElementSettings settings) : base (lesson.NameOfLesson, settings)
        {
            UpdateLesson(lesson);
        }

        public void UpdateLesson(Lesson lesson)
        {
            Lesson = lesson;

            Icon.sprite = Lesson.Icon;
            Text.text = Lesson.NameOfLesson;
        }
        public void SetActive(bool isActive)
        {
            parentObject.SetActive(isActive);
        }
    }
}