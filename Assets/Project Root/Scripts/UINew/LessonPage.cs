using Lessons;
using Localization;
using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace UINew
{
    public sealed class LessonPage : Page
    {

        public event Action BackClicked;

        public event Action StartClicked;


        public override void Init()
        {

            base.Init();


            Button backButton = document.GetButton("back-button");

            backButton.clicked += () => BackClicked?.Invoke();


            Button startButton = document.GetButton("start-button");

            startButton.clicked += () => StartClicked?.Invoke();
        }


        public void ViewLesson(Lesson lesson)
        {

            Label lessonName = document.GetLabel("lesson-name");

            lessonName.text = lesson.NameOfLesson;


            LessonTextContent content = lesson.Content[Languages.Russian];

            ViewContent(content);
        }


        private void ViewContent(LessonTextContent content)
        {

            VisualElement intro = document.GetElement("introduction");

            ViewContentElement(intro, 1, "What's this?", content.Introduction);



            VisualElement usage = document.GetElement("usage");

            ViewContentElement(usage, 2, "How use it?", content.Usage);



            VisualElement practice = document.GetElement("practice");

            ViewContentElement(practice, 3, "Train", content.PracticeDescription);
        }


        private void ViewContentElement(VisualElement element, int number,
            string title, string description)
        {

            SetTitle(element, number, title);

            SetContent(element, number, description);
        }


        private void SetTitle(VisualElement element, int number, string title)
        {

            element.GetLabel("content-number").text = number.ToString();

            element.GetLabel("content-title").text = title;
        }


        private void SetContent(VisualElement element, int iconNumber, string description)
        {

            element.GetLabel("description").text = description;
        }
    }
}
