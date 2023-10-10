using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using NcmToolManager.Library.Models;
using NcmToolManager.Library.Functions;
using System.Data;
using System.Windows.Input;

namespace NcmToolManager.Library.DataAccess
{
    public class SqlServerAccess : IDataAccess
    {
        //TODO - Create methods GetUserSalt() and GetUserPass() and StoreUserPass()
        public static UserModel ReadUserFromDb( UserModel user )
        {
            var input = new DynamicParameters();
            input.Add("@UserName", user.UserName);
            UserModel fullUser = new();
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                fullUser = (UserModel)connection.QuerySingle<UserModel>("SELECT * FROM Users WHERE UserName = @UserName", input);
            }
            return fullUser;
        }
        public void CreateDb()
        {

            //Creates the Database
            string sqlCreateDb = "DROP DATABASE IF EXISTS [NcmToolManagerDb]; CREATE DATABASE [NcmToolManagerDb]";
            using (var connection = new SqlConnection(GlobalConfig.InitialSqlCnnString()))
            {
                connection.Execute(sqlCreateDb);
                connection.Close();
            }

            //Creates table Users with a default admin user for first run
            var param = new DynamicParameters();
            UserModel admin = new ("Administrator", "admin", "", 0);
            PasswordModel password = PasswordHandling.EncryptPassword("admin");
            admin.Password = password.Password;
            admin.Salt = password.Salt;

            param.Add("@Name", admin.Name);
            param.Add("@UserName", admin.UserName);
            param.Add("@Password", password.Password);
            param.Add("@Salt", password.Salt);
            param.Add("@Role", admin.Role);

            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                connection.Execute("USE NcmToolManagerDb; Create TABLE Users ( Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, Name NVARCHAR(50) NOT NULL, LastName NVARCHAR(50), UserName NVARCHAR(50), Password NVARCHAR(50), Salt NVARCHAR(50), Role INT NOT NULL); INSERT INTO Users (Name, UserName, Password, Salt, Role) VALUES (@Name , @UserName , @Password , @Salt , @Role);", param);
                connection.Close();
            }
            

            //TODO - finish the database initialization


        }
    }
}
       

