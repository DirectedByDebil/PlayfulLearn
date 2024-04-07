using System.Collections.Generic;
using UnityEngine;

namespace LearningProgramSystem
{
    [CreateAssetMenu(fileName ="New Table Of Learning Programs", menuName = "Table Of Learning Programs", order = 60)]
    public class TableOfLearningPrograms : ScriptableObject
    {
        public List<LearningProgram> LearningPrograms { get { return _learningPrograms; } }
        
        public LearningProgram _lastLearningProgram;

        [SerializeField] private List<LearningProgram> _learningPrograms = new();
    }
}