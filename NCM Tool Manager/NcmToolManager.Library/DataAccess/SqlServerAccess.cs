using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using NcmToolManager.Library.Models;

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
            StringBuilder usersTableSql = new StringBuilder();
            usersTableSql.Append("USE NcmToolManagerDb; ");
            usersTableSql.Append("Create TABLE Users ( ");
            usersTableSql.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
            usersTableSql.Append(" Name NVARCHAR(50) NOT NULL, ");
            usersTableSql.Append(" LastName NVARCHAR(50), ");
            usersTableSql.Append(" UserName NVARCHAR(50), ");
            usersTableSql.Append(" Password NVARCHAR(50), ");
            usersTableSql.Append(" Role INT NOT NULL ");
            usersTableSql.Append("); ");
            usersTableSql.Append("INSERT INTO Users (Name, UserName, Password, Role) VALUES ");
            usersTableSql.Append("(N'admin', N'admin', N'admin', N'0');");
            string sqlUsersTable = usersTableSql.ToString();
            
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                connection.Execute(sqlUsersTable);
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
       

