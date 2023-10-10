using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using NcmToolManager.Library.Models;
using System.Data;

namespace NcmToolManager.Library.DataAccess
{
    public class SqlServerAccess : IDataAccess
    {
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
            //admin.Name = "admin";
            //admin.UserName = "admin";
            //admin.Password = "admin";
            //admin.Salt = 01234;
            //admin.Role = 0;
            
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                connection.Execute("USE NcmToolManagerDb; Create TABLE Users ( Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, Name NVARCHAR(50) NOT NULL, LastName NVARCHAR(50), UserName NVARCHAR(50), Password NVARCHAR(50), Salt NVARCHAR(50), Role INT NOT NULL); INSERT INTO Users (Name, UserName, Password, Salt, Role) VALUES (N'@Name', N'@UserName', N'@Password', N'@Salt', N'@Role');", param, commandType: CommandType.Text);
            }
            

            //TODO - finish the database initialization

            //Creates table Tools
            StringBuilder toolsTableSql = new StringBuilder();
            toolsTableSql.Append("USE NcmToolManagerDb; ");
            toolsTableSql.Append("Create TABLE Tools ( ");
            toolsTableSql.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
            toolsTableSql.Append(" Name NVARCHAR(50) ");
            toolsTableSql.Append(" LastName NVARCHAR(50) ");
            toolsTableSql.Append(" UserName NVARCHAR(50) ");
            toolsTableSql.Append(" Password NVARCHAR(50) ");
            toolsTableSql.Append(" Role INT ");
            toolsTableSql.Append("); ");
            toolsTableSql.Append("INSERT INTO Users (Name, UserName, Password, Role) VALUES ");
            toolsTableSql.Append("(admin, admin, admin, 0);");
            string sqlToolsTable = toolsTableSql.ToString();

        }
    }
}
       

