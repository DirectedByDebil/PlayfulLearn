using UnityEngine;
using LearningPrograms;

namespace UserInterface.Buttons
{
    public sealed class LearningProgramButton : ButtonElement
    {
        public LearningProgramButton(LearningProgram program, Vector2 size):base(program.name, size)
        {
            Text.text = program.name;
            Image.sprite = program.ProgramBackground;
        }
    }
}