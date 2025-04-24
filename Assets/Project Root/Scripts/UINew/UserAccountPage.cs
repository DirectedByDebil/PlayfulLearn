using Web;
using UnityEngine.UIElements;
using System;

namespace UINew
{
    public sealed class UserAccountPage : Page
    {

        public event Action CloseClicked;


        public override void Init()
        {

            base.Init();


            Button close = document.GetButton("close-button");

            close.RegisterCallback<ClickEvent>(OnCloseClicked);


            VisualElement closeArea = document.GetElement("close-area");

            closeArea.RegisterCallback<ClickEvent>(OnCloseClicked);
        }


        public void ViewUser(UserData user)
        {

            Label userName = document.GetLabel("user-name");

            userName.text = user.UserName;


            Label email = document.GetLabel("email");

            email.text = user.Email;
        }


        private void OnCloseClicked(ClickEvent e)
        {

            Hide();
            //CloseClicked?.Invoke();
        }
    }
}
