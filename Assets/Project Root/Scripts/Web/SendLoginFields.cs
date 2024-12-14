using System;

namespace Web
{

    [Serializable]
    public struct SendLoginFields
    {

        public string Email;

        public byte[] Password;

        public string DateTime;
    }
}
