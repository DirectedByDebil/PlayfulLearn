using LessonsPractices;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace UINew
{
    public sealed class TestPageModel : LessonPracticePageModel
    {

        public TestPageModel(UIDocument document) : base(document)
        {

        }


        public override void SetInputs(LessonPractice practice, IList<InputField> fields)
        {

            foreach (Question question in practice.Questions)
            {

                InputField textField = GetTestInput(question);

                fields.Add(textField);
            }
        }


        private InputField GetTestInput(Question question)
        {

            VisualElement element = document.GetElement(question.VisualElementName);


            VisualElement picture = element.GetElement("picture");

            picture.style.backgroundImage = new StyleBackground(question.Picture);


            InputField field = new()
            {

                TextField = element.GetTextField("input-field")
            };


            return field;
        }
    }
}
