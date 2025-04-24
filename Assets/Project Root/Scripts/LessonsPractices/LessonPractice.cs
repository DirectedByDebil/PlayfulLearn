using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

namespace LessonsPractices
{
    [Serializable]
    public struct LessonPractice
    {

        public string NameOfLesson;

        public VisualTreeAsset Asset;

        public PracticeType PracticeType;


        public List<Question> Questions;


        public string SceneName;

        public List<CodeLine> CodeLines;
    }
}
