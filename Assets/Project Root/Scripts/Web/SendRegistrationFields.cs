using System;

namespace Web
{

    [Serializable]
    public struct SendRegistrationFields
    {

        public string UserName;

        public string Email;

        public byte[] Password;

        public string DateTime;
    }
}
