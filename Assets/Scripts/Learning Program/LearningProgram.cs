using System.Collections.Generic;
using UnityEngine;
using LessonSystem;

namespace LearningProgramSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Learning Program", menuName = "LearningProgram", order = 70)]
    public class LearningProgram : ScriptableObject
    {
        public Sprite ProgramBackground { get { return _programBackground; } }
        public List<LearningProgramDescription> Description { get { return _description; } }

        public List<Lesson> Lessons { get { return _lessons; } }

        [SerializeField] private Sprite _programBackground;

        [Space, SerializeField] private List<LearningProgramDescription> _description = new ();
        [SerializeField] private List<Lesson> _lessons = new ();
    }
}