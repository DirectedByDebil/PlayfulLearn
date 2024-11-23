using UnityEngine;
using Web.Users;

namespace Web
{
    public sealed class AuthorizationObserver : MonoBehaviour
    {

        [SerializeField]
        
        private LoginPage _loginPage;


        [SerializeField]
        
        private RegistrationPage _registrationPage;


        private void OnEnable()
        {

            _loginPage.RegisterClicked += OnRegisterClicked;

            _loginPage.AsStudentClicked += OnAsStudentClicked;

            _loginPage.LoginClicked += OnLoginClicked;
        }


        private void OnDisable()
        {

            _loginPage.RegisterClicked -= OnRegisterClicked;

            _loginPage.AsStudentClicked -= OnAsStudentClicked;

            _loginPage.LoginClicked -= OnLoginClicked;
        }


        private void OnRegisterClicked()
        {

            _loginPage.Hide();

            _registrationPage.ShowUp();
        }


        private void OnAsStudentClicked()
        {

            EnterAsUser(UserRoles.Student);
        }


        private void EnterAsUser(UserRoles role)
        {

        }



        private void OnLoginClicked(LoginData data)
        {

            bool isValid = IsLoginValid(data);
            

            if(isValid)
            {

                EnterAsUser(UserRoles.Teacher);
            }
            else
            {

                _loginPage.OnResult(Results.Fail);
            }
        }



        private bool IsLoginValid(LoginData data)
        {

            return false;
        }
    }
}
