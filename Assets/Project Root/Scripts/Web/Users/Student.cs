
namespace Web.Users
{
    public sealed class Student : User
    {

        public Student(string name) : base(name)
        {

            Role = UserRoles.Student;
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