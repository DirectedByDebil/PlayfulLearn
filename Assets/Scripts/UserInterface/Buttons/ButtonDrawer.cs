using UnityEngine;
using Lessons;
using LearningPrograms;

namespace UserInterface.Buttons
{
    public class ButtonDrawer
    {
        private Vector2 _slotSize;
        private RectTransform _placeForContent;

        public ButtonDrawer(Vector2 slotSize, RectTransform placeForContent)
        {
            _slotSize = slotSize;
            _placeForContent = placeForContent;
        }

        public ButtonElement DrawButton(object shit, Vector2 localPosition)
        {
            ButtonElement button = null;

            if (shit is Lesson lesson)
            {
                button = new LessonButton(lesson, _slotSize);
            }
            else if (shit is LearningProgram program)
            {
                button = new LearningProgramButton(program, _slotSize);
            }

            button.rectTransform.SetParent(_placeForContent, true);
            button.rectTransform.localPosition = localPosition;
            
            return button;
        }
    }
}