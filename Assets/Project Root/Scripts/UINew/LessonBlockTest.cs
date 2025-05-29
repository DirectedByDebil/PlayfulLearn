using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace UINew
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class LessonBlockTest : Page
    {

        [SerializeField, Space]
        private List<string> _conditions;


        [SerializeField, Space]
        private List<string> _actions;


        [SerializeField, Space]
        private List<string> _animations;


        private void Start()
        {

            Init();
        }


        public override void Init()
        {

            base.Init();


            List<VisualElement> conditions = new (document.GetElement("conditions").Children());

            List<VisualElement> actions = new (document.GetElement("actions").Children());

            List<VisualElement> animations = new (document.GetElement("animations").Children());


            SetSelectedBlocks(conditions[0], actions[0], animations[0]);


            SetElements(conditions, "Поле для условия", _conditions);

            SetElements(actions, "Поле для действия", _actions);

            SetElements(animations, "Поле для анимации", _animations);
        }


        private void SetSelectedBlocks(params VisualElement[] blocks)
        {

            foreach (VisualElement element in blocks)
            {

                VisualElement root = element.GetElement("root");

                root.SwapStyle("gray-colored", "accent-colored");

                root.AddToClassList("white-text");
            }
        }


        private void SetElements(IReadOnlyList<VisualElement> elements, string headerTitle,
            IReadOnlyList<string> contents)
        {

            Label header = elements[0].GetLabel("label");
            
            header.SwapStyle("small", "medium");

            header.text = headerTitle;


            elements[1].GetLabel("label").text = contents[0];
        
            elements[2].GetLabel("label").text = contents[1];
        }
    }
}
