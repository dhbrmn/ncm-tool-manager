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

namespace NcmToolManager.Library.DataAccess
{
    public class SqlServerAccess : IDataAccess
    {
        //TODO - Create methods GetUserSalt() and GetUserPass() and StoreUserPass()
        public void CreateDb()
        {

            //Creates the Database
            string sqlCreateDb = "DROP DATABASE IF EXISTS [NcmToolManagerDb]; CREATE DATABASE [NcmToolManagerDb]";
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                connection.Execute(sqlCreateDb);
            }

            //Creates table Users with a default admin user for first run
            var param = new DynamicParameters();
            UserModel admin = new UserModel();
            admin.Name = "admin";
            admin.UserName = "admin";
            admin.Password = PasswordHandling.EncryptPassword("admin", out string salt);
            admin.Salt = salt;
            admin.Role = 0;

            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                connection.Execute("USE NcmToolManagerDb; Create TABLE Users ( Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, Name NVARCHAR(50) NOT NULL, LastName NVARCHAR(50), UserName NVARCHAR(50), Password NVARCHAR(50), Salt NVARCHAR(50), Role INT NOT NULL); INSERT INTO Users (Name, UserName, Password, Salt, Role) VALUES (N'@Name', N'@UserName', N'@Password', N'@Salt', N'@Role');", param, commandType: CommandType.Text);
            }
            

            //TODO - finish the database initialization


        }
    }
}
       

