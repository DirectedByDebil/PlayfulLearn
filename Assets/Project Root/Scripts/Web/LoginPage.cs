using UnityEngine;
using UnityEngine.UIElements;
using Extensions;
using System;

namespace Web
{

    [RequireComponent(typeof(UIDocument))]
    public sealed class LoginPage : MonoBehaviour,
        
        IPage, IResultable
    {

        public event Action AsStudentClicked
        {

            add
            {

                _asStudentButton.clicked += value;
            }

            remove
            {

                _asStudentButton.clicked -= value;
            }
        }


        public event Action RegisterClicked
        {

            add
            {

                _registerButton.clicked += value;
            }

            remove
            {

                _registerButton.clicked -= value;
            }
        }


        public event Action<FormLoginFields> LoginClicked;


        private Button _asStudentButton;

        private Button _loginButton;

        private Button _registerButton;


        private Label _loginErrorLabel;


        private TextField _emailField;

        private TextField _passwordField;


        private VisualElement _root;


        private void OnValidate()
        {

            UIDocument doc = GetComponent<UIDocument>();


            _root = doc.rootVisualElement;

            _asStudentButton = UI.GetButton(doc, "asStudentButton");

            _loginButton = UI.GetButton(doc, "loginButton");

            _registerButton = UI.GetButton(doc, "registerButton");


            _loginErrorLabel = UI.GetLabel(doc, "loginErrorLabel");


            _emailField = UI.GetTextField(doc, "emailField");
            
            _passwordField = UI.GetTextField(doc, "passwordField");
        }


        private void OnEnable()
        {

            _loginButton.clicked += OnLoginClicked;
        }


        private void OnDisable()
        {

            _loginButton.clicked -= OnLoginClicked;
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

                    _loginErrorLabel.style.visibility = Visibility.Hidden;
                    break;
                
                case Results.Warning:
                    break;
                
                case Results.Fail:

                    _loginErrorLabel.style.visibility = Visibility.Visible;
                    break;
            }
        }


        private void OnLoginClicked()
        {

            //#TODO check fields not null

            FormLoginFields data = new ()
            {

                Email = _emailField.text,
                
                Password = _passwordField.text
            };


            LoginClicked?.Invoke(data);
        }
    }
}