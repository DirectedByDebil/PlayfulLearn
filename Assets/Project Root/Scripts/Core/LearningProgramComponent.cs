using System;
using System.Collections.Generic;
using LearningPrograms;
using UserInterface;

namespace Core
{
    public sealed class LearningProgramComponent
    {

        public event Action<LearningProgram> ProgramChanged;


        private readonly List<ExpandedButton> _buttons;


        private readonly List<SelectableModel
            
            <LearningProgram>> _programs;


        private LearningProgram _currentProgram;


        public LearningProgramComponent(
            
            IReadOnlyList<ExpandedButton> buttons)
        {

            _buttons = new List<ExpandedButton>(buttons);


            _programs = new List<SelectableModel<
                
                LearningProgram>>(_buttons.Count);


            InitializeModels();
        }


        #region Set/Unset Component

        public void SetComponent()
        {

            for(int i = 0; i < _buttons.Count; i++)
            {

                SetButton(_buttons[i], _programs[i]);
            }
        }


        public void UnsetComponent()
        {

            for (int i = 0; i < _buttons.Count; i++)
            {

                UnsetButton(_buttons[i], _programs[i]);
            }
        }

        #endregion


        public void AddButton(ExpandedButton button,
            
            LearningProgram learningProgram)
        {

            SelectableModel<LearningProgram> program = new();


            InitializeButton(button, program, learningProgram);

            SetButton(button, program);


            _buttons.Add(button);

            _programs.Add(program);
        }


        public void SetLearningProgram(LearningProgram program)
        {

            _currentProgram = program;

            ProgramChanged?.Invoke(_currentProgram);
        }


        public void LoadLearningPrograms(
            
            IReadOnlyCollection<LearningProgram> programs)
        {

            int index = 0;


            foreach(LearningProgram program in programs)
            {

                ExpandedButton button = _buttons[index];


                InitializeButton(button, _programs[index],

                    program);

                
                index++;
            }
        }


        public string GetCurrentProgramName()
        {

            return _currentProgram.NameOfProgram;
        }


        #region Set/Unset Buttons

        private void SetButton(ExpandedButton button,
            
            SelectableModel<LearningProgram> program)
        {

            button.onClick.AddListener(

                program.SelectObject);


            program.Selected += TryUpdateProgram;
        }


        private void UnsetButton(ExpandedButton button,
            
            SelectableModel<LearningProgram> program)
        {

            button.onClick.RemoveListener(

                program.SelectObject);


            program.Selected -= TryUpdateProgram;
        }

        #endregion


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


        private void InitializeButton(ExpandedButton button,
            
            SelectableModel<LearningProgram> selectable,
            
            LearningProgram program)
        {

            selectable.SetSelectable(program);


            button.UpdateIcon(program.Icon);

            button.UpdateText(program.NameOfProgram);
        }
    }
}