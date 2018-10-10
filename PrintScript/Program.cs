using PrintScript.Model;
using PrintScript.Services;
using Sap.Data.Hana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintScript
{
    class Program
    {
        static SqlService s = new SqlService();

        static void Main(string[] args)
        {
            s.ExecuteQuery();
            Console.ReadKey();
        }
    }
}
