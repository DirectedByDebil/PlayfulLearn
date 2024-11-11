using System.Collections.Generic;
using UserInterface;
using Lessons;
using Extensions;
using Core;
using System;

namespace LearningPrograms
{
    public sealed class LearningProgramEditorModel
    {

        public event Action<LearningProgramData> LearningProgramCreated;


        private readonly List<ExpandedToggle> _toggles;

        private readonly List<ToggleModel<Lesson>> _models;


        private List<Lesson> _lessonsInProgram;


        private string _iconPath;


        public LearningProgramEditorModel(
            
            IReadOnlyList<ExpandedToggle> toggles)
        {

            _toggles = new List<ExpandedToggle>(toggles);

            _models = new List<ToggleModel<Lesson>>(_toggles.Count);
        }


        public void InitializeToggles(IReadOnlyCollection<Lesson> allLessons)
        {

            _lessonsInProgram = new List<Lesson>(allLessons.Count);


            foreach(Lesson lesson in allLessons)
            {

                _models.Add(new ToggleModel<Lesson>(lesson));
            }
        }


        public void AddToggle(ExpandedToggle toggle,
            
            Lesson lesson)
        {

            _toggles.Add(toggle);


            ToggleModel<Lesson> model = new (lesson);

            SetToggleModel(toggle, model);


            _models.Add(model);
        }


        public void SetModel()
        {

            for(int i = 0; i < _models.Count; i++)
            {

                SetToggleModel(_toggles[i], _models[i]);
            }
        }


        public void UnsetModel()
        {

            for (int i = 0; i < _models.Count; i++)
            {

                UnsetToggleModel(_toggles[i], _models[i]);
            }
        }


        public void CreateProgram(string nameOfProgram)
        {

            string iconName = FileExtensions.LoadFile(

                _iconPath, PathKeeper.LearningProgramsIconPath);


            LearningProgramData data = new(nameOfProgram,

                iconName,
                
                GetLessonsInProgram());;


            string fileName = string.Format(
                
                "{0}/{1}.json", PathKeeper.LearningProgramsPath,
                
                nameOfProgram);
            

            FileExtensions.WriteJson(data, fileName);


            LearningProgramCreated?.Invoke(data);
        }


        public void SetIconPath(string iconPath)
        {

            _iconPath = iconPath;
        }


        #region Set/Unset ToggleModel

        private void SetToggleModel(ExpandedToggle toggle,

            ToggleModel<Lesson> model)
        {

            toggle.onValueChanged.AddListener(

                 model.InvokeChanged);


            model.Changed += OnToggleChanged;
        }


        private void UnsetToggleModel(ExpandedToggle toggle,

           ToggleModel<Lesson> model)
        {

            toggle.onValueChanged.RemoveListener(

                 model.InvokeChanged);


            model.Changed -= OnToggleChanged;
        }

        #endregion


        private void OnToggleChanged(Lesson lesson, bool isOn)
        {

            if(isOn)
            {

                _lessonsInProgram.Add(lesson);
            }
            else
            {
                _lessonsInProgram.Remove(lesson);
            }
        }


        private List<string> GetLessonsInProgram()
        {

            List<string> lessons = new(_lessonsInProgram.Count);


            foreach (Lesson lesson in _lessonsInProgram)
            {

                lessons.Add(lesson.NameOfLesson);
            }


            return lessons;
        }
    }
}
