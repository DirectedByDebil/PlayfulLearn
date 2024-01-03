using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewLessonsDB", menuName = "LessonDB", order = 54)]
public class TableOfLessons : ScriptableObject
{
    public List<Lesson> Lessons = new ();
}