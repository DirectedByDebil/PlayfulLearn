using System.Collections.Generic;

namespace LessonsPractices.Blocks
{
    public sealed class BlockPracticeModel
    {

        private IBlocksPractice _currentPractice;


        public BlockPracticeModel()
        {

        }


        public void SetPractice(IBlocksPractice practice)
        {

            _currentPractice = practice;

            _currentPractice.Init();
        }


        public void SetInputs(IEnumerable<InputField> fields)
        {

            _currentPractice.SetInputs(fields);
        }


        public void Unload()
        {

            _currentPractice?.Unload();
        }


        public bool IsCompleted()
        {

            _currentPractice.StartGame();

            return _currentPractice.IsCompleted();
        }
    }
}
