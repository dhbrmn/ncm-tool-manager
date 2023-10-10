using NcmToolManager.Library.DataAccess;
using NcmToolManager.Library.Functions;

namespace NcmToolManager.TestingConsole
{
    class Program
    {
        public static void Main( string[] args )
        {

            Console.WriteLine("Testing DB creation");
            //var db = new SqlServerAccess();
            //db.CreateDb();

            var pass = new PasswordEncription();
            string salt;
            string password = pass.EncryptPassword("test1", out salt);
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}