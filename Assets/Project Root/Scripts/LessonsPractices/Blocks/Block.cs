using UnityEngine;
using System;

namespace LessonsPractices.Blocks
{

    [Serializable]
    public class Block : IComparable<Block>
    {

        public BlockType BlockType;


        public ConditionType ConditionType;

        public ActionType ActionType;

        public AnimationType AnimationType;


        public bool IsAnswer;


        [TextArea(1, 4)]
        public string Description;


        public int CompareTo(Block other)
        {
            return BlockType.CompareTo(other.BlockType);
        }
    }
}