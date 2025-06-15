using LessonsPractices;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace UINew
{
    public sealed class MiniGamePageModel : SeparatedPagesModel
    {

        private readonly VisualTreeAsset _codeLineAsset;


        public MiniGamePageModel(UIDocument document, VisualTreeAsset codeLineAsset): base(document)
        {

            _codeLineAsset = codeLineAsset;
        }


        public override void SetInputs(LessonPractice practice, IList<InputField> fields)
        {

            practiceRoot.Clear();


            for (int i = 0; i < practice.CodeLines.Count; i++)
            {

                VisualElement element = _codeLineAsset.Instantiate();


                CodeLine codeLine = practice.CodeLines[i];

                InputField field = GetCodeInput(element, codeLine, i + 1);


                if (!string.IsNullOrEmpty(codeLine.Description))
                {

                    fields.Add(field);
                }

                practiceRoot.Add(element);
            }
        }


        private InputField GetCodeInput(VisualElement root,
            CodeLine codeLine, int lineNumber)
        {

            Label lineNumberLabel = root.GetLabel("line-number");

            lineNumberLabel.text = lineNumber.ToString();


            TextField lineText = root.GetTextField("line-text");

            lineText.value = codeLine.Code;


            SetFieldReadOnly(lineText, codeLine.IsReadOnly);


            InputField field = new()
            {
                TextField = lineText,

                Description = codeLine.Description
            };

            return field;
        }
    }
}
