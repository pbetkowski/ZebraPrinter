using PrintScript.Model;
using PrintScript.Services;
using Sap.Data.Hana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace PrintScript
{
    class Program
    {
        static SqlService SqlExe = new SqlService();

        static void Main(string[] args)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(300);

            var timer = new System.Threading.Timer((e) =>
            {
                Console.WriteLine("Scanning for objects...");
                SqlExe.ExecuteQuery();
            }, null, startTimeSpan, periodTimeSpan);

            Console.ReadKey();

        }
    }
}
