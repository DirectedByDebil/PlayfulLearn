
namespace Users
{
    public sealed class Administrator : User
    {
        public Administrator(string name) : base(name)
        {
            Role = UserRoles.Administrator;
        }

        public override void LogOut()
        {
            throw new System.NotImplementedException();
        }

        public override void TryToLogIn()
        {
            throw new System.NotImplementedException();
        }
    }
}