using LearningPrograms;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

namespace UINew
{

    [RequireComponent(typeof(UIDocument))]
    public sealed class AllLearningProgramsPage : Page
    {

        public event Action<LearningProgram> LearningProgramClicked;


        [SerializeField, Space]
        private VisualTreeAsset _itemTemplate;


        public override void Init()
        {

            base.Init();


            VisualElement close = document.GetElement("transparent-panel");

            close.RegisterCallback<ClickEvent>(StartClosing);
        }


        public void ViewLearningPrograms(IList<LearningProgram> learningPrograms)
        {

            ListView listView = document.GetListView("programs-listview");


            listView.makeItem = () => _itemTemplate.Instantiate();


            listView.bindItem = (item, index) =>
            {

                LearningProgram program = learningPrograms[index];

                BindItem(item, program);
            };

            listView.itemsSource = new List<LearningProgram>(learningPrograms);
        }


        private void BindItem(VisualElement item, LearningProgram program)
        {

            Label label = item.GetLabel("name");

            label.text = program.NameOfProgram;


            ProgressBar progress = item.GetProgressBar("progress");

            progress.title = program.CompletionPercent + "%";

            progress.value = program.CompletionPercent;


            VisualElement icon = item.GetElement("icon");

            icon.style.backgroundImage = new StyleBackground(program.Icon);


            item.RegisterCallback((ClickEvent e) => 
            {

                LearningProgramClicked?.Invoke(program);
            });
        }
    }
}
