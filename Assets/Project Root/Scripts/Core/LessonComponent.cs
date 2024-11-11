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

        public event Action<Lesson> LessonChanged;


        private readonly List<ExpandedButton> _buttons;


        private readonly List<SelectableModel<

            Lesson>> _models;


        private Lesson _currentLesson;


        public LessonComponent(
            
            IReadOnlyList<ExpandedButton> lessonButtons)
        {

            _buttons = new List<ExpandedButton>(lessonButtons);


            _models = new List<SelectableModel<

                Lesson>>(_buttons.Count);


            InitializeModels();
        }


        #region Subscribe/Unsubscribe On Selected Events

        public void SetComponent()
        {

            for(int i = 0; i < _buttons.Count; i++)
            {

                SetButton(_buttons[i], _models[i]);
            }
        }


        public void UnsetComponent()
        {

            for (int i = 0; i < _buttons.Count; i++)
            {

                UnsetButton(_buttons[i], _models[i]);
            }
        }

        #endregion


        public void OnLearningProgramChanged(LearningProgram program)
        {

            IReadOnlyList<Lesson> lessons = program.Lessons;


            for(int i = 0; i < lessons.Count; i++)
            {

                Lesson lesson = lessons[i];

                UpdateButton(i, lesson);
            }


            for(int k = lessons.Count; k < _buttons.Count; k++)
            {

                _buttons[k].gameObject.SetActive(false);
            }
        }


        public void AddButton(ExpandedButton button,
            
            Lesson lesson)
        {

            SelectableModel<Lesson> model = new();


            InitializeButton(button, model, lesson);

            SetButton(button, model);


            _buttons.Add(button);

            _models.Add(model);
        }


        private void InitializeModels()
        {

            for(int i = 0; i < _buttons.Count; i++)
            {

                _models.Add(new SelectableModel<Lesson>());
            }
        }


        #region Set/Unset Button
 
        private void SetButton(IClickable button,
            
            SelectableModel<Lesson> model)
        {

            button.onClick.AddListener(
                
                model.SelectObject);


            model.Selected += TryUpdateLesson;
        }


        private void UnsetButton(IClickable button,
            
            SelectableModel<Lesson> model)
        {

            button.onClick.RemoveListener(

                model.SelectObject);


            model.Selected -= TryUpdateLesson;
        }

        #endregion


        private void TryUpdateLesson(Lesson lesson)
        {

            if(lesson != _currentLesson)
            {

                _currentLesson = lesson;

                LessonChanged?.Invoke(_currentLesson);
            }
        }


        private void UpdateButton(int index, Lesson lesson)
        {

            ExpandedButton button = _buttons[index];


            if (!button.gameObject.activeInHierarchy)
            {

                button.gameObject.SetActive(true);
            }


            InitializeButton(button, _models[index], lesson);
        }


        private void InitializeButton(ExpandedButton button,

            SelectableModel<Lesson> selectable,

            Lesson lesson)
        {

            selectable.SetSelectable(lesson);


            button.UpdateIcon(lesson.Icon);

            button.UpdateText(lesson.NameOfLesson);
        }
    }
}
