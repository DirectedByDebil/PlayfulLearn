using UnityEngine;
using System;

namespace LessonsPractices
{

    [Serializable]
    public struct CodeLine
    {

        [TextArea(1, 4)]
        public string Code;

        public bool IsReadOnly;

        public string Description;
    }
}
