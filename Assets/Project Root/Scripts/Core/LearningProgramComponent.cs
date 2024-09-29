using System;
using System.Collections.Generic;
using LearningPrograms;
using UserInterface;

namespace Core
{
    public sealed class LearningProgramComponent
    {

        public event Action<NewLearningProgram> ProgramChanged;


        private readonly IReadOnlyList<ExpandedButton> _buttons;


        private readonly List<SelectableModel
            
            <NewLearningProgram>> _programs;


        private NewLearningProgram _currentProgram;


        public LearningProgramComponent(
            
            IReadOnlyList<ExpandedButton> buttons)
        {

            _buttons = buttons;


            _programs = new List<SelectableModel<
                
                NewLearningProgram>>(_buttons.Count);


            InitializeModels();
        }


        #region Subscribe/Unsubscribe on

        public void SetComponent()
        {

            for(int i = 0; i < _buttons.Count; i++)
            {

                _buttons[i].onClick.AddListener(

                    _programs[i].SelectObject);


                _programs[i].Selected += TryUpdateProgram;
            }
        }


        public void UnsetComponent()
        {

            for (int i = 0; i < _buttons.Count; i++)
            {

                _buttons[i].onClick.RemoveListener(

                    _programs[i].SelectObject);


                _programs[i].Selected -= TryUpdateProgram;
            }
        }

        #endregion


        public void SetLearningProgram(NewLearningProgram program)
        {

            _currentProgram = program;

            ProgramChanged?.Invoke(_currentProgram);
        }


        public void LoadLearningPrograms(
            
            IReadOnlyCollection<NewLearningProgram> programs)
        {

            int index = 0;


            foreach(NewLearningProgram program in programs)
            {

                _programs[index].SetSelectable(program);


                ExpandedButton button = _buttons[index];

                button.UpdateIcon(program.Icon);

                button.UpdateText(program.NameOfProgram);


                index++;
            }
        }


        public string GetCurrentProgramName()
        {

            return _currentProgram.NameOfProgram;
        }


        private void TryUpdateProgram(NewLearningProgram program)
        {

            if (_currentProgram != program)
            {

                SetLearningProgram(program);
            }
        }


        private void InitializeModels()
        {

            for(int i = 0; i < _buttons.Count; i++)
            {

                _programs.Add(new SelectableModel<
                    
                    NewLearningProgram>());
            }
        }
    }
}