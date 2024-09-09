using LearningPrograms;

namespace UserInterface.Buttons
{
    public sealed class LearningProgramButton : ButtonElement
    {
        public LearningProgramButton(LearningProgram program,
            
            ButtonElementSettings settings) : base(program.name, settings)
        {

            Text.text = program.name;

            Icon.sprite = program.ProgramBackground;
        }
    }
}