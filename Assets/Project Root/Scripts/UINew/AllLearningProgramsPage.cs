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

        public event Action CloseClicked;


        [SerializeField, Space]
        private VisualTreeAsset _itemTemplate;


        public override void Init()
        {

            base.Init();


            VisualElement close = document.GetElement("transparent-panel");

            close.RegisterCallback((ClickEvent e) => CloseClicked?.Invoke());
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

            Label label = item.GetLabel("label");

            label.text = program.NameOfProgram;


            VisualElement icon = item.GetElement("icon");

            icon.style.backgroundImage = new StyleBackground(program.Icon);
        }
    }
}
