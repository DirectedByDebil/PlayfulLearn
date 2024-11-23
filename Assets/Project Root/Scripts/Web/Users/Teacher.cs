
namespace Web.Users
{
    public sealed class Teacher : User
    {

        public Teacher(string name) : base(name)
        {

            Role = UserRoles.Teacher;
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