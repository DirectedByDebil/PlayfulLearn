using UnityEngine.UIElements;
using System;
using System.Collections.Generic;
using LessonsPractices.Blocks;

namespace LessonsPractices
{
    [Serializable]
    public struct LessonPractice
    {

        public PracticeType PracticeType;


        public VisualTreeAsset TestPage;

        public List<Question> Questions;


        public string SceneName;

        public VisualTreeAsset MiniGameInfo;

        public List<CodeLine> CodeLines;


        public List<Block> Blocks;


        public IReadOnlyList<Block> GetBlocksByType(BlockType blockType)
        {

            return Blocks.FindAll(block => block.BlockType == blockType);
        }
    }
}
