using UnityEngine;
using LearningProgramSystem;

namespace Presentation
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