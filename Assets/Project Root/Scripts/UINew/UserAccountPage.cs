using Core;
using Web;
using Playables;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace UINew
{
    public sealed class UserAccountPage : Page
    {

        [SerializeField, Space]
        private VisualTreeAsset _userIconTemplate;


        private VisualElement _userIcon;


        public override void Init()
        {

            base.Init();


            _userIcon = document.GetElement("user-icon");


            Button close = document.GetButton("close-button");

            close.RegisterCallback<ClickEvent>(StartClosing);
        }


        public void ViewUser(UserData user)
        {

            Label userName = document.GetLabel("user-name");

            userName.text = user.UserName;


            Label email = document.GetLabel("email");

            email.text = user.Email;
        }


        #region View Characters

        public void ViewSelectedCharacter(Character character)
        {

            _userIcon.style.backgroundImage = new StyleBackground(character.Icon);
        }


        public void ViewCharacters(IReadOnlyCollection<Character> characters)
        {

            ScrollView scrollView = document.GetScrollView("scroll-view");


            foreach (Character character in characters)
            {

                VisualElement userIcon = _userIconTemplate.Instantiate();

                ViewCharacter(userIcon, character);


                scrollView.Add(userIcon);
            }
        }


        private void ViewCharacter(VisualElement userIcon, Character character)
        {

            userIcon.AddToClassList("icon-character");


            userIcon.RegisterCallback<ClickEvent>((e) => 
            {
                OnSelectedCharacterChanged(character);}
            );


            VisualElement icon = userIcon.GetElement("character-icon");

            icon.style.backgroundImage = new StyleBackground(character.Icon);


            Label name = userIcon.GetLabel("character-name");

            name.text = character.Name;
        }


        private void OnSelectedCharacterChanged(Character character)
        {

            ViewSelectedCharacter(character);

            SessionData.SetCharacter(character);
        }

        #endregion
    }
}
