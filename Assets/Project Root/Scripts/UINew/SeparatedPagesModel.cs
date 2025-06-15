using UnityEngine.UIElements;

namespace UINew
{
    public abstract class SeparatedPagesModel : LessonPracticePageModel
    {

        protected VisualElement theoryRoot;

        protected VisualElement practiceRoot;


        private Button _theoryButton;

        private Button _practiceButton;


        public SeparatedPagesModel(UIDocument document) : base(document)
        {

        }


        public void SetTheoryInfo(VisualTreeAsset infoAsset)
        {

            VisualElement info = infoAsset.Instantiate();

            info.AddToClassList("mini-game-info");


            theoryRoot.Clear();

            theoryRoot.Insert(0, info);

            ShowTheory(new ClickEvent());
        }


        protected override void UpdateElements()
        {

            base.UpdateElements();

            practiceRoot = document.GetElement("practice-root");

            theoryRoot = document.GetElement("theory-root");


            _theoryButton?.UnregisterCallback<ClickEvent>(ShowTheory);

            _practiceButton?.UnregisterCallback<ClickEvent>(ShowPractice);


            _theoryButton = document.GetButton("theory-button");

            _practiceButton = document.GetButton("practice-button");


            _theoryButton.RegisterCallback<ClickEvent>(ShowTheory);

            _practiceButton.RegisterCallback<ClickEvent>(ShowPractice);
        }


        protected void ShowTheory(ClickEvent e)
        {

            theoryRoot.Show();

            practiceRoot.Hide();
        }


        protected void ShowPractice(ClickEvent e)
        {

            theoryRoot.Hide();

            practiceRoot.Show();
        }
    }
}
