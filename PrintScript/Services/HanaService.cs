using Sap.Data.Hana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintScript.Services
{
    public static class HanaService
    {
        public static HanaConnection ConnectToDataBase()
        {
            HanaConnection connection;

            connection = new HanaConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Hana"].ConnectionString);
            return connection;
        }
    }
}
