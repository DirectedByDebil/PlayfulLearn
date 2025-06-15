using LessonsPractices;
using LessonsPractices.Blocks;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System;
using System.Collections.Generic;

namespace UINew
{
    public sealed class BlockPageModel : SeparatedPagesModel
    {

        public event Action<ConditionType> ConditionClicked;

        public event Action<ActionType> ActionClicked;
        
        public event Action<AnimationType> AnimationClicked;


        private readonly VisualTreeAsset _blockAsset;


        private VisualElement _conditionsRoot;

        private VisualElement _actionsRoot;

        private VisualElement _animationsRoot;


        public BlockPageModel(UIDocument document, VisualTreeAsset blockAsset) : base(document)
        {

            _blockAsset = blockAsset;
        }


        public override void SetInputs(LessonPractice practice, IList<InputField> fields)
        {

            var conditions = practice.GetBlocksByType(BlockType.Condition);

            var actions = practice.GetBlocksByType(BlockType.Action);

            var animations = practice.GetBlocksByType(BlockType.Animation);
            

            fields.AddRange(GetBlocks(_conditionsRoot, conditions));

            fields.AddRange(GetBlocks(_actionsRoot, actions));

            fields.AddRange(GetBlocks(_animationsRoot, animations));
        }


        protected override void UpdateElements()
        {

            base.UpdateElements();

            _conditionsRoot = document.GetElement("conditions");

            _actionsRoot = document.GetElement("actions");

            _animationsRoot = document.GetElement("animations");
        }


        private IReadOnlyList<InputField> GetBlocks(VisualElement root,
            IReadOnlyCollection<Block> blocks)
        {

            root.Clear();


            List<InputField> buttons = new (blocks.Count);


            foreach (Block block in blocks)
            {

                VisualElement element = _blockAsset.Instantiate();


                Label label = element.GetLabel("label");

                label.text = block.Description;

                root.Add(element);


                InputField field = new()
                {

                    Block = block,

                    Button = element.GetButton("block-button")
                };


                buttons.Add(field);
            }

            return buttons;
        }
    }
}
