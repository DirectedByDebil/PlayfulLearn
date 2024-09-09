
namespace Core
{
    public sealed class EducationSystem
    {

        private readonly LessonComponent _lessons;

        private readonly LearningProgramComponent _learningPrograms;


        public EducationSystem(LessonComponent lessons,
            
            LearningProgramComponent learningProgram)
        {

            _lessons = lessons;

            _learningPrograms = learningProgram;
        }


        public void SetSystem()
        {

            _learningPrograms.ProgramChanged += 
                
                _lessons.OnLearningProgramChanged;
        }


        public void UnsetSystem()
        {

            _learningPrograms.ProgramChanged -=

                _lessons.OnLearningProgramChanged;
        }
    }
}
