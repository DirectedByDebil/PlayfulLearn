using UnityEngine.UI;

namespace UserInterface
{
    public interface ICloseable
    {

        public Button.ButtonClickedEvent CloseClicked
        {

            get;
        }
    }
}