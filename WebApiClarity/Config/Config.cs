using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiClarity.Configuration {
    public class Config {

        public static string providerName = "System.Data.SqlClient";
        
        public static string authServer = "localhost";
        public static string authDbName = "ClarityHealth";
        public static int authSessionExpirationLengthInSeconds = 10;

        public static string connectionStringName(){
            return "jlapc";
        }

        public static string SQLConnectionFor(string user, string pass) {
            string conn =
                "Data Source=" + authServer + ";Initial Catalog=" + authDbName + ";Integrated Security=False;" +
                "User Id="+   user +
                ";Password="+ pass +
                ";MultipleActiveResultSets=True";
            return conn;
        }

    }
}