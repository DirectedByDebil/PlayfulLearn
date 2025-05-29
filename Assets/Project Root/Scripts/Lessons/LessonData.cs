using Localization;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Lessons
{
    [Serializable]
    public struct LessonData
    {

        [SerializeField]
        public string NameOfLesson;

        public string IconName;


        public List<LessonNode> Contents;

        public List<LessonTheory> Theory;


        public LessonData(string nameOfLesson,

            string iconName,
            
            List<LessonNode> contents)
        {

            NameOfLesson = nameOfLesson;

            IconName = iconName;

            Contents = contents;

            Theory = new List<LessonTheory>();
        }
    }
}
