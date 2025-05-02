using Lessons;
using Localization;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

namespace UINew
{
    public sealed class LessonPage : Page
    {

        public event Action StartClicked;


        [SerializeField, Space]
        private List<LessonPictures> _pictures;


        public override void Init()
        {

            base.Init();


            Button backButton = document.GetButton("back-button");

            backButton.RegisterCallback<ClickEvent>(StartClosing);


            Button startButton = document.GetButton("start-button");

            startButton.clicked += () => StartClicked?.Invoke();
        }


        public void ViewLesson(Lesson lesson)
        {

            Label lessonName = document.GetLabel("lesson-name");

            lessonName.text = lesson.NameOfLesson;


            LessonPictures pictures = _pictures.Find
                (picture => picture.NameOfLesson == lesson.NameOfLesson);


            LessonTextContent content = lesson.Content[Languages.Russian];

            ViewContent(content, pictures);
        }


        #region View Lesson Content

        private void ViewContent(LessonTextContent content, LessonPictures pictures)
        {

            VisualElement intro = document.GetElement("introduction");

            ViewIntro(intro, content.Introduction);

            ViewPicture(intro, pictures.Introduction);


            VisualElement usage = document.GetElement("usage");

            ViewUsage(usage, content.Usage);

            ViewPicture(usage, pictures.Usage);


            VisualElement practice = document.GetElement("practice");

            ViewPracticeDescription(practice, content.PracticeDescription);

            ViewPicture(practice, pictures.PracticeDescription);
        }


        private void ViewIntro(VisualElement root, string introduction)
        {

            ViewContentElement(root, 1, "Что это?", introduction);
        }


        private void ViewUsage(VisualElement root, string usage)
        {

            ViewContentElement(root, 2, "Как использовать?", usage);
        }


        private void ViewPracticeDescription(VisualElement root, string description)
        {

            ViewContentElement(root, 3, "Научимся!", description);
        }
        
        #endregion


        private void ViewPicture(VisualElement root, Sprite sprite)
        {

            VisualElement picture = root.GetElement("picture");

            picture.style.backgroundImage = new StyleBackground(sprite);
        }


        private void ViewContentElement(VisualElement element, int number,
            string title, string description)
        {

            element.GetLabel("content-number").text = number.ToString();

            element.GetLabel("content-title").text = title;


            element.GetLabel("description").text = description;
        }
    }
}
