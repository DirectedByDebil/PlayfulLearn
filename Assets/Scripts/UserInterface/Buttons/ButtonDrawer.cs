using UnityEngine;
using Lessons;
using LearningPrograms;

namespace UserInterface.Buttons
{
    public class ButtonDrawer
    {
        private RectTransform _placeForContent;
        private ButtonElementSettings _settings;
        
        public ButtonDrawer(ButtonElementSettings settings, RectTransform placeForContent)
        {
            _settings = settings;
            _placeForContent = placeForContent;
        }

        public ButtonElement DrawButton(object shit, Vector2 localPosition)
        {
            ButtonElement button = null;

            if (shit is Lesson lesson)
            {
                button = new LessonButton(lesson, _settings);
            }
            else if (shit is LearningProgram program)
            {
                button = new LearningProgramButton(program, _settings);
            }

            button.rectTransform.SetParent(_placeForContent, true);
            button.rectTransform.localPosition = localPosition;
            
            return button;
        }
    }
}