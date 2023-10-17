using NcmToolManager.Library.DataAccess;
using NcmToolManager.Library.Functions;
using NcmToolManager.Library.Models;

namespace NcmToolManager.TestingConsole
{
    class Program
    {
        public static void Main( string[] args )
        {

            //Console.WriteLine("Testing DB creation");
            //var db = new SqlServerAccess();
            //db.CreateDb();
            //Console.WriteLine("Done");
            //Console.ReadLine();

            //Console.WriteLine("Testing password hash generation");
            //PasswordModel password = PasswordHandling.EncryptPassword("test1");
            //Console.WriteLine("Hash salt value:"+password.Salt);
            //Console.WriteLine("Hash password value:"+password.Password);
            //Console.WriteLine("Done");
            //Console.ReadLine();

            //Console.WriteLine("Testing password verification");
            //Console.WriteLine("Input username:");
            //string userName;
            //string password;
            //userName = Console.ReadLine();
            //Console.WriteLine("Input password:");
            //password = Console.ReadLine();
            //Console.WriteLine("Your username is: " + userName);
            //Console.WriteLine("Your password is: " + password);
            //if (PasswordHandling.VerifyUser(userName))
            //{
            //    Console.WriteLine("We found a user with this username in the database!");
            //    if (PasswordHandling.VerifyPassword(userName, password))
            //    {
            //        Console.WriteLine("Your password is correct!");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Your password is incorrect!");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("There is no user with this username!");
            //}

            //LoginModel loginModel = new LoginModel();
            //loginModel.UserName = "admin";
            //SqlServerAccess.ReadLoginFromDb( loginModel );


            //string username = "";
            //string password = "";
            //SqlServerAccess.NewLogin( username, password );

            //string name = "  ";
            //string lastName = "  ";
            //SqlServerAccess.NewUser(name, lastName);

            string manuName = "";
            SqlServerAccess.NewManufacturer( manuName );
        }
    }
}