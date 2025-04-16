using Lessons;
using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace UINew
{
    public sealed class LessonPracticePage : Page
    {

        public event Action FinishClicked;

        private Button _finishButton;


        public override void Init()
        {

            base.Init();

        }


        public void PreparePractice(Lesson lesson)
        {

            _finishButton = document.GetButton("finish-button");

            _finishButton?.RegisterCallback<ClickEvent>(OnFinishClicked);
        }


        private void OnFinishClicked(ClickEvent e)
        {

            _finishButton.UnregisterCallback<ClickEvent>(OnFinishClicked);

            FinishClicked?.Invoke();
        }
    }
}
