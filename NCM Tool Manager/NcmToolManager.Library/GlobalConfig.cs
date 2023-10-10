using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcmToolManager.Library
{
    public static class GlobalConfig
    {
        public static string InitialSqlCnnString()
        {
            string cnnString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";
            return cnnString;
        }

        public static string SqlCnnString()
        {
            string cnnString = @"Server=localhost\SQLEXPRESS;Database=NcmToolManagerDb;Trusted_Connection=True;";
            return cnnString;
        }

    }
}
