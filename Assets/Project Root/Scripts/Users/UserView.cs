using UnityEngine;
using UnityEngine.UI;
using UserInterface;

namespace Users
{
    public sealed class UserView : MonoBehaviour,

        ICloseable
    {

        public Button.ButtonClickedEvent CloseClicked
        {

            get => _closeButton.onClick;
        }


        [SerializeField, Space]

        private Button _closeButton;
    }
}
