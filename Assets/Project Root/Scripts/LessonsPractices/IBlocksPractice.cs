using System.Collections.Generic;

namespace LessonsPractices
{
    public interface IBlocksPractice : IPractice
    {

        public void SetInputs(IEnumerable<InputField> inputs);
    }
}
