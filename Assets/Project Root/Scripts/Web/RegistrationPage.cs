using UnityEngine;
using UnityEngine.UIElements;
using Extensions;
using System;

namespace Web
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class RegistrationPage : MonoBehaviour,
        
        IPage, IResultable
    {

        public event Action BackClicked
        {

            add
            {

                _backButton.clicked += value;
            }

            remove
            {

                _backButton.clicked -= value;
            }
        }

        public event Action<RegistrationData> RegisterClicked;


        private Button _backButton;

        private Button _registerButton;


        private TextField _userNameField;

        private TextField _emailField;

        private TextField _password1Field;

        private TextField _password2Field;


        private Label _passwordsErrorLabel;


        private VisualElement _root;


        private void OnValidate()
        {

            UIDocument doc = GetComponent<UIDocument>();

            _root = doc.rootVisualElement;


            _backButton = UI.GetButton(doc, "back-button");

            _registerButton = UI.GetButton(doc, "register-button");


            _userNameField = UI.GetTextField(doc, "user-name");

            _emailField = UI.GetTextField(doc, "email");

            _password1Field = UI.GetTextField(doc, "password1");

            _password2Field = UI.GetTextField(doc, "password2");


            _passwordsErrorLabel = UI.GetLabel(doc, "passwords-error-label");
        }


        private void OnEnable()
        {

            _registerButton.clicked += OnRegisterClicked;
        }


        private void OnDisable()
        {

            _registerButton.clicked -= OnRegisterClicked;
        }


        public void ShowUp()
        {

            _root.style.display = DisplayStyle.Flex;
        }


        public void Hide()
        {

            _root.style.display = DisplayStyle.None;
        }


        public void OnResult(Results result)
        {

            switch (result)
            {
                case Results.Success:

                    _passwordsErrorLabel.style.visibility = Visibility.Hidden;
                    break;


                case Results.Warning:
                    break;

                case Results.Fail:

                    _passwordsErrorLabel.style.visibility = Visibility.Visible;
                    break;
            }

        }



        private void OnRegisterClicked()
        {

            //#TODO check fields not null
            //#TODO check passwords are same


            RegistrationData data = new ()
            {

                UserName = _userNameField.text,

                Email = _emailField.text,

                Password = _password1Field.text
            };


            RegisterClicked?.Invoke(data);
        }
    }
}
