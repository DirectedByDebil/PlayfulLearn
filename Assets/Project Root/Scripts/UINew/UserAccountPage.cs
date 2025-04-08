using UnityEngine;
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
        }


        private void OnCloseClicked(ClickEvent e)
        {

            CloseClicked?.Invoke();
        }
    }
}
