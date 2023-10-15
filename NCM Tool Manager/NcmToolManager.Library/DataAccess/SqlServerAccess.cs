using Dapper;
using System.Data.SqlClient;
using NcmToolManager.Library.Models;
using NcmToolManager.Library.Functions;
using System.Security.Cryptography.Xml;

namespace NcmToolManager.Library.DataAccess
{
    public class SqlServerAccess : IDataAccess
    {
        //TODO - Create methods GetUserSalt() and GetUserPass() and StoreUserPass()
        public static LoginModel ReadLoginFromDb( LoginModel credentials )
        {
            var input = new DynamicParameters();
            input.Add("@UserName", credentials.UserName);
            LoginModel fullCredentials = new();
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                fullCredentials = (LoginModel)connection.QuerySingle<LoginModel>("SELECT * FROM Users WHERE UserName = @UserName", input);
            }
            return fullCredentials;
        }
        public void CreateDb()
        {

            //Creates the Database
            string sqlCreateDb = "DROP DATABASE IF EXISTS [NcmToolManagerDb]; CREATE DATABASE [NcmToolManagerDb]";
            using (var connection = new SqlConnection(GlobalConfig.InitialSqlCnnString()))
            {
                connection.Execute(sqlCreateDb);
            }

            //Creates table Logins with a default admin login for first run
            
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                var loginParam = new DynamicParameters();
                PasswordModel password = PasswordHandling.EncryptPassword("admin");
                LoginModel admin = new("admin", password, 0);
                loginParam.Add("@UserName", admin.UserName);
                loginParam.Add("@Password", password.Password);
                loginParam.Add("@Salt", password.Salt);
                loginParam.Add("@Role", admin.Role);

                connection.Execute("USE NcmToolManagerDb; Create TABLE Logins ( Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, UserName NVARCHAR(50), Password NVARCHAR(50), Salt NVARCHAR(50), Role INT NOT NULL); INSERT INTO Logins (UserName, Password, Salt, Role) VALUES (@UserName , @Password , @Salt , @Role);", loginParam);
            }

            //Creates table Users and links the admin login to the user

            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                var userParam = new DynamicParameters();
                LoginModel admin = ReadLoginFromDb(new LoginModel { UserName = "admin" });
                UserModel adminUser = new("Administrator", "Administrator");

                userParam.Add("@Name", adminUser.Name);
                userParam.Add("@LastName", adminUser.LastName);
                userParam.Add("@LoginId", admin.Id);

                connection.Execute("USE NcmToolManagerDb; Create TABLE Users ( Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, Name NVARCHAR(50) NOT NULL, LastName NVARCHAR(50) NOT NULL, LoginId INT, FOREIGN KEY (LoginId) REFERENCES Logins (Id)); INSERT INTO Users (Name, LastName, LoginId) VALUES (@Name, @LastName, @LoginId)", userParam);
            }

            //Crates table Manufacturers
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                connection.Execute("USE NcmToolManagerDb; Create TABLE Manufacturers ( Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, Name NVARCHAR(50) NOT NULL);");
            }

            //Crates table Sellers
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {

            }

            //Crates table Tools

            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {

            }

            //Crates table Serials
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                connection.Execute("USE NcmToolManagerDb; Create TABLE Serials");
            }




                //TODO - finish the database initialization


            }
    }
}
       

