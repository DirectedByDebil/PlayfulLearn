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


        public override void Init()
        {

            base.Init();


            Button openPrograms = document.GetButton("open-programs-button");
            
            openPrograms.RegisterCallback<ClickEvent>(OnOpenProgramsClicked);


            Button openUser = document.GetButton("open-user-button");

            openUser.RegisterCallback<ClickEvent>(OnUserAccountClicked);
        }


        public void ViewLearningProgram(LearningProgram program)
        {

            Label label = document.GetLabel("current-program-name");

            label.text = program.NameOfProgram;


            VisualElement icon = document.GetElement("current-program-icon");

            icon.style.backgroundImage = new StyleBackground(program.Icon);


            ViewLessons(program.Lessons);
        }


        public void ViewLessons(IReadOnlyCollection<Lesson> lessons)
        {

            VisualElement parent = document.GetElement("lessons-parent");

            parent.Clear();


            foreach (Lesson lesson in lessons)
            {

                VisualElement element = _itemTemplate.Instantiate();


                SetElement(element, lesson);
               

                parent.Add(element);
            }


            ScrollView scrollView = document.GetScrollView("lessons-scrollview");

            scrollView.Add(parent);
        }


        private void SetElement(VisualElement element, Lesson lesson)
        {

            Label label = element.GetLabel("label");

            label.text = lesson.NameOfLesson;


            VisualElement icon = element.GetElement("icon");


            if(lesson.IsCompleted)
            {

                icon.AddToClassList("completed");
            }
            else
            {

                icon.style.backgroundImage = new StyleBackground(lesson.Icon);
            }


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
