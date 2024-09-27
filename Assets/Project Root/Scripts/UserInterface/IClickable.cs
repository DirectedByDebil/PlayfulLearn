using UnityEngine.UI;

namespace UserInterface
{
    public interface IClickable
    {

        public Button.ButtonClickedEvent onClick
        {
            get;
        }
    }
}
