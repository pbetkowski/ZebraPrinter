using PrintScript.Model;
using Sap.Data.Hana;
using System;
using System.Collections.Generic;
using System.Data;
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

        public static void CallUpdateProcedure(HanaConnection connection, int docEntry, int result)
        {
            try
            {
                HanaCommand cmd = new HanaCommand(Queries.UpdateResult, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DocEntry", SqlDbType.Int).Value = docEntry;
                cmd.Parameters.Add("@Result", SqlDbType.Int).Value = result;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                CallUpdateProcedure(connection, docEntry, 3);
            }
        }
    }
}
