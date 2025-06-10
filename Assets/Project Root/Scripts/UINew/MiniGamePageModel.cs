using LessonsPractices;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UINew
{
    public sealed class MiniGamePageModel : LessonPracticePageModel
    {

        private readonly VisualTreeAsset _codeLineAsset;


        private Button _theoryButton;

        private Button _practiceButton;

        private VisualElement _codeRoot;

        private VisualElement _theoryRoot; 



        public MiniGamePageModel(UIDocument document, VisualTreeAsset codeLineAsset): base(document)
        {

            _codeLineAsset = codeLineAsset;
        }



        public void SetMiniGameInfo(VisualTreeAsset infoAsset)
        {

            VisualElement info = infoAsset.Instantiate();

            info.AddToClassList("mini-game-info");


            _theoryRoot.Clear();

            _theoryRoot.Insert(0, info);

            ShowTheory(new ClickEvent());
        }


        public override void SetInputs(LessonPractice practice, IList<InputField> fields)
        {

            _codeRoot.Clear();


            for (int i = 0; i < practice.CodeLines.Count; i++)
            {

                VisualElement element = _codeLineAsset.Instantiate();


                CodeLine codeLine = practice.CodeLines[i];

                InputField field = GetCodeInput(element, codeLine, i + 1);


                if (!string.IsNullOrEmpty(codeLine.Description))
                {

                    fields.Add(field);
                }

                _codeRoot.Add(element);
            }
        }


        protected override void UpdateElements()
        {

            base.UpdateElements();

            _codeRoot = document.GetElement("code-root");

            _theoryRoot = document.GetElement("theory-root");


            _theoryButton?.UnregisterCallback<ClickEvent>(ShowTheory);

            _practiceButton?.UnregisterCallback<ClickEvent>(ShowPractice);


            _theoryButton = document.GetButton("theory-button");

            _practiceButton = document.GetButton("practice-button");


            _theoryButton.RegisterCallback<ClickEvent> (ShowTheory);

            _practiceButton.RegisterCallback<ClickEvent> (ShowPractice);
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


        private void ShowTheory(ClickEvent e)
        {

            _theoryRoot.Show();

            _codeRoot.Hide();
        }


        private void ShowPractice(ClickEvent e)
        {

            _theoryRoot.Hide();

            _codeRoot.Show();
        }
    }
}
