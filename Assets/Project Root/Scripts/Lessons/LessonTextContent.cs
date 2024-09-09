using UnityEngine;
using System;

namespace Lessons
{
    [Serializable]
    public struct LessonTextContent
    {

        public string Introduction
        {

            get => _introduction;
        }


        public string Usage
        {

            get => _usage;
        }


        public string PracticeDescription
        {

            get => _practiceDescription;
        }


        [TextArea(5, 20), SerializeField] 
        
        private string _introduction;


        [TextArea(5, 20), SerializeField] 
        
        private string _usage;
        
        
        [TextArea(5, 20), SerializeField] 
        
        private string _practiceDescription;


        public LessonTextContent(string intro,
            
            string usage, string practice)
        {

            _introduction = intro;

            _usage = usage;

            _practiceDescription = practice;
        }
    }
}