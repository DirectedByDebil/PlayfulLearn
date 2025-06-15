using UINew;
using UnityEngine.UIElements;
using System;

namespace LessonsPractices.Blocks
{

    public class BlockButton
    {

        public event Action Clicked
        {

            add => _button.clicked += value;

            remove => _button.clicked -= value;
        }


        #region Types

        public BlockType BlockType
        {
            get => _block.BlockType;
        }


        public ConditionType ConditionType
        {
            get => _block.ConditionType;
        }


        public ActionType ActionType
        {
            get => _block.ActionType;
        }


        public AnimationType AnimationType
        { 
            get => _block.AnimationType;
        }

        #endregion


        public bool IsAnswer
        {
            get => _block.IsAnswer;
        }



        private readonly Block _block;

        private readonly Button _button;


        public BlockButton(Block block, Button button)
        {

            _block = block;

            _button = button;
        }


        public void SetSelected(bool isSelected)
        {

            _button.SwapIf(isSelected, "selected-block");
        }
    }
}
