using LessonsPractices.Blocks;
using UnityEngine.UIElements;
using System;

namespace LessonsPractices
{

    [Serializable]
    public struct InputField
    {

        public TextField TextField;

        public string Description;

        public Block Block;

        public Button Button;
    }
}
