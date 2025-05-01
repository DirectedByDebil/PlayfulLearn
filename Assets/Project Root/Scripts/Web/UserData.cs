using System;
using System.Collections.Generic;

namespace Web
{
    [Serializable]
    public struct UserData
    {

        public string Email;

        public string UserName;

        public string SelectedCharacter;

        public List<string> CompletedLessons;
    }
}
