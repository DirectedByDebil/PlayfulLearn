using System.Collections.Generic;
using UnityEngine;
using Lessons;
using Localization;

namespace LearningPrograms
{
    [CreateAssetMenu(fileName = "New Learning Program",
        
        menuName = "LearningProgram", order = 70)]
    public class LearningProgram : ScriptableObject
    {

        //#TODO rename it to Icon
        public Sprite ProgramBackground
        {

            get => _programBackground;
        }


        public List<LearningProgramDescription> Description
        {
            
            get => _description;
        }


        public List<Lesson> Lessons
        {

            get => _lessons;
        }


        //#TODO make Name property

        [SerializeField]
        
        private Sprite _programBackground;


        [Space, SerializeField]
        
        private List<LearningProgramDescription> _description = new ();


        [SerializeField]
        
        private List<Lesson> _lessons = new ();


        public void SetProgram(LearningProgramData template)
        {

            //_lessons = template.lessons;
        }
    }
}