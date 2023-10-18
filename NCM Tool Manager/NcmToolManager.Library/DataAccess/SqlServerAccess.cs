using Dapper;
using System.Data.SqlClient;
using NcmToolManager.Library.Models;
using NcmToolManager.Library.Functions;
using System.Security.Cryptography.Xml;
using System;
using System.Xml.Linq;

namespace NcmToolManager.Library.DataAccess
{
    public class SqlServerAccess : IDataAccess
    {
        //TODO - Create methods NewSeller(), NewSalesPerson(), NewTool(), NewSerial()
        public static void NewSeller(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new NullReferenceException("A seller needs a name.");
            }
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                var input = new DynamicParameters();
                input.Add("@Name", name);

                connection.Execute("USE NcmToolManagerDb; INSERT INTO Sellers (Name) VALUES (@Name);", input);
            }
        }
        /// <summary>
        /// Creates a new database entry in the manufacturers table
        /// </summary>
        /// <param name="name">String input for the name of the manufacturer</param>
        /// <exception cref="NullReferenceException">Returns if there is no name input</exception>
        public static void NewManufacturer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new NullReferenceException("A manufacturer needs a name.");
            }
                using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
                {
                    var input = new DynamicParameters();
                    input.Add("@Name", name);

                    connection.Execute("USE NcmToolManagerDb; INSERT INTO Manufacturers (Name) VALUES (@Name);", input);
                }
        }
        /// <summary>
        /// Creates a new database entry in the users table
        /// </summary>
        /// <param name="name">String input for a name</param>
        /// <param name="lastName">String input for a surname</param>
        /// <exception cref="NullReferenceException">Returns if the name and surname are empty or blank.</exception>
        public static void NewUser(string name, string lastName)
        {
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(lastName))
            {
                throw new NullReferenceException("A user needs both a name and a surname.");
            }
                using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
                {
                    var input = new DynamicParameters();
                    input.Add("@Name", name);
                    input.Add("@LastName", lastName);

                    connection.Execute("USE NcmToolManagerDb; INSERT INTO Users (Name, LastName) VALUES (@Name, @LastName);", input);
                }
        }
        /// <summary>
        /// Creates a new database entry in the logins table
        /// </summary>
        /// <param name="username">User input username</param>
        /// <param name="password">User input password</param>
        /// <exception cref="NullReferenceException">Returns exception if the username or password or both are empty or null.</exception>
        public static void NewLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password))
            {
                throw new NullReferenceException("Both username and password needed to create new login credentials.");
            }
                using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
                {
                    PasswordModel encryptedPassword = PasswordHandling.EncryptPassword(password);
                    var input = new DynamicParameters();
                    input.Add("@UserName", username);
                    input.Add("@Password", encryptedPassword.Password);
                    input.Add("@Salt", encryptedPassword.Salt);
                    input.Add("@Role", 1);

                    connection.Execute("USE NcmToolManagerDb; INSERT INTO Logins (UserName, Password, Salt, Role) VALUES (@UserName, @Password, @Salt, @Role);", input);
                }
        }
        /// <summary>
        /// Reads database using the username of a LoginModel
        /// </summary>
        /// <param name="credentials">Input object LoginModel requires at least the username field in order for the search to execute</param>
        /// <returns>Full login model with hashed password and hashed password salt</returns>
        public static LoginModel ReadLoginFromDb( LoginModel credentials )
        {
            if (string.IsNullOrWhiteSpace(credentials.UserName))
            {
                throw new NullReferenceException("No username to search the database with.");
            }
                var input = new DynamicParameters();
                input.Add("@UserName", credentials.UserName);
                LoginModel fullCredentials = new();
                    using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
                    {
                        fullCredentials = connection.QuerySingle<LoginModel>("SELECT * FROM Logins WHERE UserName = @UserName", input);
                    }
                return fullCredentials;
        }
        /// <summary>
        /// Creates a new database and tables. Also creates  a default "Admin" user for first time use.
        /// </summary>
        public static void CreateDb()
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

                connection.Execute("USE NcmToolManagerDb; Create TABLE Logins ( Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, UserName NVARCHAR(50) UNIQUE NOT NULL, Password NVARCHAR(50) NOT NULL, Salt NVARCHAR(50) NOT NULL, Role INT NOT NULL); INSERT INTO Logins (UserName, Password, Salt, Role) VALUES (@UserName , @Password , @Salt , @Role);", loginParam);
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
                connection.Execute("USE NcmToolManagerDb; Create TABLE Sellers (Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, Name NVARCHAR(50) NOT NULL, Address NVARCHAR(50), City NVARCHAR(50), PostalCode NVARCHAR(50), Country NVARCHAR(50));");
            }

            //Creates table SalesPeople
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                connection.Execute("USE NcmToolManagerDb; Create TABLE SalesPeople (Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, Name NVARCHAR(50) NOT NULL, LastName NVARCHAR(50) NOT NULL, Email NVARCHAR(50), Phone NVARCHAR(50), SellerId INT NOT NULL, FOREIGN KEY (SellerId) REFERENCES Sellers (Id));");
            }

            //Creates table SellManuLink
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                connection.Execute("USE NcmToolManagerDb; Create TABLE SellManuLink (Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, SellerId INT NOT NULL, ManufacturerId INT NOT NULL, FOREIGN KEY (SellerId) REFERENCES Sellers (Id), FOREIGN KEY (ManufacturerId) REFERENCES Manufacturers (Id));");
            }

            //Crates table Tools
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                connection.Execute("USE NcmToolManagerDb; Create TABLE Tools (Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, Name NVARCHAR(50) NOT NULL, Description NVARCHAR(255), ManufacturerId INT, FOREIGN KEY (ManufacturerId) REFERENCES Manufacturers (Id));");
            }

            //Crates table Serials
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                connection.Execute("USE NcmToolManagerDb; Create TABLE Serials (Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ToolId INT NOT NULL, UserId INT, FOREIGN KEY (ToolId) REFERENCES Tools (Id), FOREIGN KEY (UserId) REFERENCES Users (Id));");
            }
        }
    }
}
       

