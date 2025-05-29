using LessonsPractices;
using UnityEngine;
using System;

namespace Lessons
{
    [CreateAssetMenu(fileName ="New Lesson", menuName ="Objects/Lesson")]
    public sealed class LessonObject : ScriptableObject, IComparable<LessonObject>
    {

        [field: SerializeField, Space]
        public string NameOfLesson { get; private set; }


        [field: SerializeField, Space]
        public LessonDifficulties Difficulty { get; private set; }


        [field: SerializeField, Space]
        public Sprite Icon { get; private set; } 


        [field: SerializeField, Space]
        public LessonPractice Practice { get; private set; }


        public int CompareTo(LessonObject other)
        {
            return NameOfLesson.CompareTo(other.NameOfLesson);
        }
    }
}
