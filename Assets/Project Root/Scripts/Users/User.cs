
namespace Users
{
    public abstract class User
    {

        public UserRoles Role
        
        { get; protected set; }


        public string Name
        
        { get; private set; }


        public User(string name)
        {

            Name = name;
        }


        //#TODO registration in some registration manager
        public abstract void TryToLogIn();

        public abstract void LogOut();
    }
}