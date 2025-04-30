using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

namespace LessonsPractices
{
    [Serializable]
    public struct LessonPractice
    {

        public string NameOfLesson;

        public PracticeType PracticeType;


        public VisualTreeAsset TestPage;

        public List<Question> Questions;


        public string SceneName;

        public VisualTreeAsset MiniGameInfo;

        public List<CodeLine> CodeLines;
    }
}
