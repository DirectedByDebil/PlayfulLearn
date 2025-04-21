using Lessons;
using LessonsPractices;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

namespace UINew
{
    public sealed class LessonPracticePage : Page
    {

        public event Action FinishClicked;


        [SerializeField, Space]
        private List<LessonPractice> _practices;


        private Button _checkButton;

        private Button _finishButton;


        private IReadOnlyList<Question> _questions;

        private List<TextField> _inputs;


        public override void Init()
        {

            base.Init();

            _inputs = new List<TextField>();
        }


        public void PreparePractice(Lesson lesson)
        {

            //#TODO maybe better do with binary search
            LessonPractice lessonPractice = _practices.Find(
                (practice) => practice.NameOfLesson == lesson.NameOfLesson);

            document.visualTreeAsset = lessonPractice.Asset;


            _questions = lessonPractice.Questions;
            

            SetInputs(_questions);

            SetButtons();
        }


        private void SetInputs(IReadOnlyCollection<Question> questions)
        {

            _inputs.Clear();


            foreach (Question question in questions)
            {
             
                VisualElement element = document.GetElement(question.VisualElementName);

                _inputs.Add(element.GetTextField("input-field"));


                VisualElement picture = element.GetElement("picture");

                picture.style.backgroundImage = new StyleBackground(question.Picture);
            }
        }


        private void SetButtons()
        {

            _checkButton = document.GetButton("check-button");

            _checkButton?.RegisterCallback<ClickEvent>(OnCheckClicked);


            _finishButton = document.GetButton("finish-button");

            _finishButton.AddToClassList("button__disabled");
        }


        private void OnCheckClicked(ClickEvent e)
        {

            if(IsPracticeCompleted())
            {

                _finishButton.RemoveFromClassList("button__disabled");

                _finishButton?.RegisterCallback<ClickEvent>(OnFinishClicked);
            }
            else
            {

                _finishButton.AddToClassList("button__disabled");

                _finishButton?.UnregisterCallback<ClickEvent>(OnFinishClicked);
            }
        }


        private bool IsPracticeCompleted()
        {

            bool isPracticeCompleted = true;


            for(int i = 0; i < _questions.Count; i++)
            {

                TextField input = _inputs[i];


                if(input.value != _questions[i].Answer)
                {

                    input.SwapStyle("input-correct", "input-error");

                    isPracticeCompleted = false;

                }
                else
                {

                    input.SwapStyle("input-error", "input-correct");
                }
            }


            return isPracticeCompleted;
        }


        private void OnFinishClicked(ClickEvent e)
        {

            _checkButton.UnregisterCallback<ClickEvent>(OnCheckClicked);

            _finishButton.UnregisterCallback<ClickEvent>(OnFinishClicked);


            FinishClicked?.Invoke();
        }
    }
}
