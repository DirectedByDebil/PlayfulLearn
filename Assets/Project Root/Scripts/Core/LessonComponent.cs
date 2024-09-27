using System;
using System.Collections.Generic;
using Lessons;
using LearningPrograms;
using UserInterface;


namespace Core
{
    //#TODO maybe make some base ButtonManagerComponent class
    public sealed class LessonComponent
    {

        public event Action<NewLesson> LessonChanged;


        private readonly IReadOnlyList<ExpandedButton> _buttons;


        private readonly List<SelectableModel<

            NewLesson>> _models;


        private NewLesson _currentLesson;


        public LessonComponent(
            
            IReadOnlyList<ExpandedButton> lessonButtons)
        {

            _buttons = lessonButtons;


            _models = new List<SelectableModel<

                NewLesson>>(_buttons.Count);


            InitializeModels();
        }


        #region Subscribe/Unsubscribe On Selected Events

        public void SetComponent()
        {

            for(int i = 0; i < _buttons.Count; i++)
            {

                _buttons[i].onClick.AddListener(

                    _models[i].SelectObject);


                _models[i].Selected += TryUpdateLesson;
            }
        }


        public void UnsetComponent()
        {

            for (int i = 0; i < _buttons.Count; i++)
            {

                _buttons[i].onClick.RemoveListener(

                    _models[i].SelectObject);


                _models[i].Selected -= TryUpdateLesson;
            }
        }

        #endregion


        public void OnLearningProgramChanged(NewLearningProgram program)
        {

            IReadOnlyList<NewLesson> lessons = program.Lessons;


            for(int i = 0; i < lessons.Count; i++)
            {

                NewLesson lesson = lessons[i];

                UpdateButton(i, lesson);
            }


            for(int k = lessons.Count; k < _buttons.Count; k++)
            {

                _buttons[k].gameObject.SetActive(false);
            }
        }


        private void InitializeModels()
        {

            for(int i = 0; i < _buttons.Count; i++)
            {

                _models.Add(new SelectableModel<NewLesson>());
            }
        }


        private void TryUpdateLesson(NewLesson lesson)
        {

            if(lesson != _currentLesson)
            {

                _currentLesson = lesson;

                LessonChanged?.Invoke(_currentLesson);
            }
        }


        private void UpdateButton(int index, NewLesson lesson)
        {

            ExpandedButton button = _buttons[index];


            if (!button.gameObject.activeInHierarchy)
            {

                button.gameObject.SetActive(true);
            }


            button.UpdateIcon(lesson.Icon);

            button.UpdateText(lesson.NameOfLesson);


            _models[index].SetSelectable(lesson);
        }
    }
}
