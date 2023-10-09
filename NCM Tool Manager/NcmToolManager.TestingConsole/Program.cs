using NcmToolManager.Library.DataAccess;

namespace NcmToolManager.TestingConsole
{
    class Program
    {
        public static void Main( string[] args )
        {

            Console.WriteLine("Testing DB creation");
            var db = new SqlServerAccess();
            db.CreateDb();
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}