using UnityEngine;
using System;

namespace Localization
{

    [Serializable]
    public struct LearningProgramDescription
    {

        public Languages Language
        {

            get => _language;
        }


        public string Description
        {

            get => _description;
        }


        [SerializeField] private Languages _language;


        [SerializeField, TextArea(5,20)]
        
        private string _description;
    }
}