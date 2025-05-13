using Lessons;
using LearningPrograms;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

namespace UINew
{

    [RequireComponent(typeof(UIDocument))]
    public sealed class CurrentLearningProgramPage : Page
    {

        public event Action OpenLearningProgramsClicked;

        public event Action UserAccountClicked;

        public event Action<Lesson> LessonClicked;


        [SerializeField, Space]
        private VisualTreeAsset _itemTemplate;


        private Button _openUser;


        public override void Init()
        {

            base.Init();


            Button openPrograms = document.GetButton("open-programs-button");
            
            openPrograms.RegisterCallback<ClickEvent>(OnOpenProgramsClicked);


            _openUser = document.GetButton("open-user-button");

            _openUser.RegisterCallback<ClickEvent>(OnUserAccountClicked);
        }


        public void ViewLearningProgram(LearningProgram program)
        {

            Label label = document.GetLabel("current-program-name");

            label.text = program.RusName;


            VisualElement icon = document.GetElement("current-program-icon");

            icon.style.backgroundImage = new StyleBackground(program.Icon);


            ViewLessons(program.Lessons);
        }


        public void ViewLessons(IReadOnlyCollection<Lesson> lessons)
        {

            VisualElement parent = document.GetElement("lessons-parent");

            parent.Clear();


            int index = 1;

            foreach (Lesson lesson in lessons)
            {

                VisualElement element = _itemTemplate.Instantiate();


                SetElement(element, lesson, index);
               

                parent.Add(element);

                index++;
            }


            ScrollView scrollView = document.GetScrollView("lessons-scrollview");

            scrollView.Add(parent);
        }


        public void ViewUserIcon(Sprite sprite)
        {

            _openUser.style.backgroundImage = new StyleBackground(sprite);
        }


        private void SetElement(VisualElement element, Lesson lesson, int lessonNumber)
        {

            Label label = element.GetLabel("lesson-number");

            label.text = string.Format("Урок {0}:", lessonNumber);


            Label lessonName = element.GetLabel("lesson-name");

            lessonName.text = lesson.NameOfLesson;


            VisualElement icon = element.GetElement("icon");


            if(lesson.IsCompleted)
            {

                icon.SwapStyle("image-gray-accent-colored", "image-success-colored");
                
                icon.SwapStyle("icon", "icon-success");
            }
            else
            {

            }
                icon.style.backgroundImage = new StyleBackground(lesson.Icon);


            element.RegisterCallback((ClickEvent e) =>
            {

                LessonClicked?.Invoke(lesson);
            });
        }


        private void OnOpenProgramsClicked(ClickEvent e)
        {

            OpenLearningProgramsClicked?.Invoke();
        }


        private void OnUserAccountClicked(ClickEvent e)
        {

            UserAccountClicked?.Invoke();
        }
    }
}
