using System;
using System.Collections.Generic;
using LearningPrograms;
using UserInterface;

namespace Core
{
    public sealed class LearningProgramComponent
    {

        public event Action<LearningProgram> ProgramChanged;


        private readonly IReadOnlyList<ExpandedButton> _buttons;


        private readonly List<SelectableModel
            
            <LearningProgram>> _programs;


        private LearningProgram _currentProgram;


        public LearningProgramComponent(
            
            IReadOnlyList<ExpandedButton> buttons)
        {

            _buttons = buttons;


            _programs = new List<SelectableModel<
                
                LearningProgram>>(_buttons.Count);


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


        public void SetLearningProgram(LearningProgram program)
        {

            _currentProgram = program;

            ProgramChanged?.Invoke(_currentProgram);
        }


        public void LoadLearningPrograms(
            
            IReadOnlyList<LearningProgram> programs)
        {

            for(int i = 0; i < programs.Count; i++)
            {

                LearningProgram program = programs[i];

                _programs[i].SetSelectable(program);


                ExpandedButton button = _buttons[i];

                button.UpdateIcon(program.ProgramBackground);

                button.UpdateText(program.name);
            }
        }


        private void TryUpdateProgram(LearningProgram program)
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
                    
                    LearningProgram>());
            }
        }
    }
}
