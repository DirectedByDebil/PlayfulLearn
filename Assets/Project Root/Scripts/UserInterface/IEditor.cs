using UnityEngine.UI;

namespace UserInterface
{
    public interface IEditor : ICloseable
    {

        public Button.ButtonClickedEvent CreateClicked
        {

            get;
        }
    }
}
