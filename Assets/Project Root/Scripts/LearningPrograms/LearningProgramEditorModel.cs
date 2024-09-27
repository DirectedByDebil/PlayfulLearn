using System.Collections.Generic;
using UserInterface;
using Lessons;
using Extensions;

namespace LearningPrograms
{
    public sealed class LearningProgramEditorModel
    {

        private readonly IReadOnlyList<ExpandedToggle> _toggles;


        private List<ToggleModel<NewLesson>> _models;


        private List<NewLesson> _lessonsInProgram;


        public LearningProgramEditorModel(
            
            IReadOnlyList<ExpandedToggle> toggles)
        {

            _toggles = toggles;

            _models = new List<ToggleModel<NewLesson>>(_toggles.Count);
        }


        public void InitializeToggles(IReadOnlyCollection<NewLesson> allLessons)
        {

            _lessonsInProgram = new List<NewLesson>(allLessons.Count);


            foreach(NewLesson lesson in allLessons)
            {

                _models.Add(new ToggleModel<NewLesson>(lesson));
            }
        }


        public void SetModel()
        {

            for(int i = 0; i < _toggles.Count; i++)
            {

                _toggles[i].onValueChanged.AddListener(

                    _models[i].InvokeChanged);


                _models[i].Changed += OnToggleChanged;
            }
        }


        public void UnsetModel()
        {

            for (int i = 0; i < _toggles.Count; i++)
            {

                _toggles[i].onValueChanged.RemoveListener(

                    _models[i].InvokeChanged);


                _models[i].Changed -= OnToggleChanged;
            }
        }


        public void CreateProgram(string nameOfProgram)
        {

            string iconName = "IconName.png";


            LearningProgramData data = new(nameOfProgram,

                iconName,
                
                GetLessonsInProgram());;


            string fileName = string.Format(
                
                "Assets/Project Root/Learning Programs/{0}.json",
                
                nameOfProgram);
            

            FileExtensions.WriteJson(data, fileName);
        }


        private void OnToggleChanged(NewLesson lesson, bool isOn)
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


            foreach (NewLesson lesson in _lessonsInProgram)
            {

                lessons.Add(lesson.NameOfLesson);
            }


            return lessons;
        }
    }
}
