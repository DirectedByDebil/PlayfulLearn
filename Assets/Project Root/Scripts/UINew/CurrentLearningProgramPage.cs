using Lessons;
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


        public void ViewLessons(IList<Lesson> lessons)
        {

            VisualElement parent = document.GetElement("lessons-parent");


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

            label.text = string.Format("{0}", lesson.NameOfLesson);


            //Button button = element.GetButton("lesson-button");

            //button.RegisterCallback<ClickEvent>(OnClicked);


            VisualElement icon = element.GetElement("icon");

            icon.style.backgroundImage = new StyleBackground(lesson.Icon);
        }


        private void OnClicked(ClickEvent e)
        {

            if(e.currentTarget is VisualElement element)
            {

                Debug.Log("Yes");
            }
            else
            {

                Debug.Log("No");
            }
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
