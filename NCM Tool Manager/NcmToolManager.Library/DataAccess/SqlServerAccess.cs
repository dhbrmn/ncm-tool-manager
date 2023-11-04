using Dapper;
using System.Data.SqlClient;
using NcmToolManager.Library.Models;
using NcmToolManager.Library.Functions;
using System.Security.Cryptography.Xml;
using System;
using System.Xml.Linq;
using System.Linq;
using System.Configuration;
using System.Printing;
using System.Windows.Documents;
using System.Collections.Generic;

namespace NcmToolManager.Library.DataAccess
{
    public class SqlServerAccess : IDataAccess
    {
        public static List<IssuedToolModel> GetIssuedToolsQuantity()
        {
            List<IssuedToolModel> issuedTools = new List<IssuedToolModel>();
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                var toolList = connection.Query<ToolModel>("USE NcmToolManagerDb; SELECT Id, Name FROM Tools;");
                foreach (ToolModel tool in toolList)
                {
                    var n = new DynamicParameters();
                    n.Add("ToolId", tool.Id);
                    var quantity = connection.ExecuteScalar<int>("USE NcmToolManagerDb; Select COUNT(*) FROM Serials WHERE ToolId = @ToolId;", n);
                    IssuedToolModel issuedTool = new IssuedToolModel(tool.Name, quantity);
                    issuedTools.Add(issuedTool);
                }
            }
            return issuedTools;
        }
        //TODO - Create methods NewTool(), NewSerial()
        public static void SetMinimalStock( ToolModel toolModel, int minimalStock )
        {
            var n = new DynamicParameters();
            n.Add("ToolId", toolModel.Id);
            n.Add("MinimalStock", minimalStock);
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {

                connection.Execute("USE NcmToolManagerDb; INSERT INTO MinimalStock (ToolId, MinimalStock VALUES (@ToolId, @MinimalStock);", n);
            }
        }
        public static void NewSalesPerson(SalesPersonModel newSalesPerson, SellerModel designatedSeller)
        {
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                var input = new DynamicParameters();
                input.Add("@Name", newSalesPerson.Name);
                input.Add("@LastName", newSalesPerson.LastName);
                input.Add("@Email", newSalesPerson.Email);
                input.Add("@Phone", newSalesPerson.Phone);
                input.Add("@SellerId", designatedSeller.Id);

                connection.Execute("USE NcmToolManagerDb; INSERT INTO SalesPeople (Name, LastName, Email, Phone, SellerId) VALUES (@Name, @LastName, @Email, @Phone, @SellerId);", input);
            }
        }
        public static void NewSeller(SellerModel newSeller)
        {
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                var input = new DynamicParameters();
                input.Add("@Name", newSeller.Name);
                input.Add("@Address", newSeller.Address);
                input.Add("@City", newSeller.City);
                input.Add("@PostalCode", newSeller.PostalCode);
                input.Add("@Country", newSeller.Country);

                connection.Execute("USE NcmToolManagerDb; INSERT INTO Sellers (Name, Address, City, PostalCode, Country) VALUES (@Name, @Address, @City, @PostalCode, @Country);", input);
            }
        }

        public static void NewManufacturer(ManufacturerModel newManufacturer)
        {
                using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
                {
                    var input = new DynamicParameters();
                    input.Add("@Name", newManufacturer.Name);

                    connection.Execute("USE NcmToolManagerDb; INSERT INTO Manufacturers (Name) VALUES (@Name);", input);
                }
        }
     
        public static void NewUser(UserModel newUser)
        {
                using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
                {
                    var input = new DynamicParameters();
                    input.Add("@Name", newUser.Name);
                    input.Add("@LastName", newUser.LastName);

                    connection.Execute("USE NcmToolManagerDb; INSERT INTO Users (Name, LastName) VALUES (@Name, @LastName);", input);
                }
        }

        public static void NewLogin(LoginModel newLogin)
        {
                using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
                {
                    var input = new DynamicParameters();
                    input.Add("@UserName", newLogin.UserName);
                    input.Add("@Password", newLogin.Password);
                    input.Add("@Salt", newLogin.Salt);
                    input.Add("@Role", 1);

                    connection.Execute("USE NcmToolManagerDb; INSERT INTO Logins (UserName, Password, Salt, Role) VALUES (@UserName, @Password, @Salt, @Role);", input);
                }
        }
        public static UserModel ReadUserByUsername( string username )
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new NullReferenceException("No username to search the databse with.");

            var loginSearch = new DynamicParameters();
            loginSearch.Add("UserName", username);
            LoginModel loginModelPopulate = new();
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                loginModelPopulate = connection.QuerySingle<LoginModel>("SELECT * FROM Logins WHERE UserName = @UserName", loginSearch);
            }
            var userSearch = new DynamicParameters();
            userSearch.Add("LoginId", loginModelPopulate.Id);
            UserModel userModelPopulate = new();
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                userModelPopulate = connection.QuerySingle<UserModel>("SELECT * FROM Users WHERE LoginId = @LoginId", userSearch);
            }
            return userModelPopulate;
        }
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
                LoginModel admin = (new LoginModel{UserName="admin", Role=0});
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
            //Crates table MinimalStock
            using (var connection = new SqlConnection(GlobalConfig.SqlCnnString()))
            {
                connection.Execute("USE NcmToolManagerDb; Create TABLE MinimalStock (Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ToolId INT NOT NULL, MinimalStock INT, FOREIGN KEY (ToolId) REFERENCES Tools (Id));");
            }

        }
    }
}
       

