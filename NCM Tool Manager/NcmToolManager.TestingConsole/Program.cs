using NcmToolManager.Library.DataAccess;
using NcmToolManager.Library.Functions;

namespace NcmToolManager.TestingConsole
{
    class Program
    {
        public static void Main( string[] args )
        {

            //Console.WriteLine("Testing DB creation");
            //var db = new SqlServerAccess();
            //db.CreateDb();

            Console.WriteLine("Testing password hash generation");
            var pass = new PasswordHandling();
            string salt;
            string password = pass.EncryptPassword("test1", out salt);
            Console.WriteLine("Hash salt value:"+salt);
            Console.WriteLine("Hash password value:"+password);
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}