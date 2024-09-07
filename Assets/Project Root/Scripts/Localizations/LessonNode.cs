using UnityEngine;
using System;
using Lessons;

namespace Localization
{
    [Serializable]
    public struct LessonNode
    {

        public Languages Language
        {

            get  =>_language;
        }


        public LessonTextContent Texts
        {

            get =>_lessonTextContent;
        }


        [SerializeField]
        
        private Languages _language;


        [SerializeField]
        
        private LessonTextContent _lessonTextContent;


        public LessonNode(Languages language,
            
            LessonTextContent textContent)
        {

            _language = language;

            _lessonTextContent = textContent;
        }
    }
}