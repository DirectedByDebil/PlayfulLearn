﻿using UnityEngine;
using Web.Users;
using System.Net;

namespace Web
{
    public sealed class AuthorizationObserver : MonoBehaviour
    {

        [SerializeField]
        
        private LoginPage _loginPage;


        [SerializeField]
        
        private RegistrationPage _registrationPage;


        private RestService _rest;


        private void Start()
        {

            _rest = new RestService();
        }


        private void OnEnable()
        {

            _loginPage.RegisterClicked += OnRegisterClicked;

            _loginPage.AsStudentClicked += OnAsStudentClicked;

            _loginPage.LoginClicked += OnLoginClicked;


            _registrationPage.RegisterClicked += OnRegisterClicked;

            _registrationPage.BackClicked += BackToLogin;
        }


        private void OnDisable()
        {

            _loginPage.RegisterClicked -= OnRegisterClicked;

            _loginPage.AsStudentClicked -= OnAsStudentClicked;

            _loginPage.LoginClicked -= OnLoginClicked;


            _registrationPage.RegisterClicked -= OnRegisterClicked;

            _registrationPage.BackClicked -= BackToLogin;
        }


        #region Navigation

        private void OnRegisterClicked()
        {

            _loginPage.Hide();

            _registrationPage.ShowUp();
        }


        private void BackToLogin()
        {

            _loginPage.ShowUp();

            _registrationPage.Hide();
        }

        #endregion


        #region Enter As User

        private void OnAsStudentClicked()
        {

            EnterAsUser(UserRoles.Student);
        }


        private void EnterAsUser(UserRoles role)
        {

        }

        #endregion


        #region Login

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


        //#TODO should be async
        private bool IsLoginValid(LoginData data)
        {

            return false;
        }

        #endregion


        #region Registration

        private async void OnRegisterClicked(FormRegistrationFields data)
        {

            HttpStatusCode status = await _rest.RegisterAsync(data);


            switch (status)
            {

                case HttpStatusCode.OK:

                    BackToLogin();

                    break;


                case HttpStatusCode.Conflict:

                    _registrationPage.OnResult(Results.Fail);

                    break;
            }
        }

        #endregion
    }
}
