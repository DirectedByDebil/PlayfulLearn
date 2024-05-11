using System.Collections.Generic;
using Localizations;
using UnityEngine;
using UnityEditor;

namespace Lessons
{
    public struct LessonTemplate
    {
        public string nameOfLesson;
        public Sprite sprite;
        public Dictionary<Languages, LessonTextContent> content;
        public SceneAsset practiceScene;
    }
}