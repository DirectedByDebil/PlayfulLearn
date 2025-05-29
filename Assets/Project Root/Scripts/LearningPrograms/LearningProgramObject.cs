using UnityEngine;
using System;

namespace LearningPrograms
{
    [CreateAssetMenu(fileName ="New Learning Program", menuName ="Objects/LearningProgram")]
    public sealed class LearningProgramObject : ScriptableObject, IComparable<LearningProgramObject>
    {

        [field: SerializeField, Space]
        public string NameOfProgram { get; private set; }


        [field: SerializeField, Space]
        public string RusName { get; private set; }


        [field: SerializeField, Space]
        public Sprite Icon { get; private set; }


        public int CompareTo(LearningProgramObject other)
        {
            return NameOfProgram.CompareTo(other.NameOfProgram);
        }
    }
}
