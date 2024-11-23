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


        public event Action<LoginData> LoginClicked;


        private UIDocument _uiDocument;


        private Button _asStudentButton;

        private Button _loginButton;

        private Button _registerButton;


        private Label _loginErrorLabel;


        private TextField _emailField;

        private TextField _passwordField;


        private void OnValidate()
        {

            _uiDocument = GetComponent<UIDocument>();
            

            _asStudentButton = UI.GetButton(_uiDocument, "asStudentButton");

            _loginButton = UI.GetButton(_uiDocument, "loginButton");

            _registerButton = UI.GetButton(_uiDocument, "registerButton");


            _loginErrorLabel = UI.GetLabel(_uiDocument, "loginErrorLabel");


            _emailField = UI.GetTextField(_uiDocument, "emailField");
            
            _passwordField = UI.GetTextField(_uiDocument, "passwordField");
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

            gameObject.SetActive(true);
        }

        public void Hide()
        {

            gameObject.SetActive(false);
        }


        public void OnResult(Results result)
        {

            switch (result)
            {
                case Results.Success:
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

            LoginData data = new ()
            {

                Email = _emailField.text,
                
                Password = _passwordField.text
            };


            LoginClicked?.Invoke(data);
        }
    }
}