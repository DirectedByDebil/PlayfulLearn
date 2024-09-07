using System.Collections.Generic;
using Localization;
using UnityEngine;

namespace Lessons
{
    public struct LessonTemplate
    {
        public string nameOfLesson, practiceScene;
        public Sprite sprite;
        public Dictionary<Languages, LessonTextContent> content;
    }
}