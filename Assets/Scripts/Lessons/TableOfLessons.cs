using UnityEngine;
using System.Collections.Generic;

namespace Lessons
{
    [CreateAssetMenu(fileName = "NewLessonsDB", menuName = "LessonDB", order = 54)]
    public class TableOfLessons : ScriptableObject
    {
        public List<Lesson> NewLessons = new ();
    }
}