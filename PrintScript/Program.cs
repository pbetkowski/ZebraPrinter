using PrintScript.Model;
using PrintScript.Services;
using Sap.Data.Hana;
using System;


namespace PrintScript
{
    class Program
    {
        static MainController SqlExe = new MainController();

        static void Main(string[] args)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(10);

            var timer = new System.Threading.Timer((e) =>
            {
                Console.WriteLine("Scanning for objects...");
                SqlExe.ExecuteQuery();
            }, null, startTimeSpan, periodTimeSpan);

            Console.ReadKey();

        }
    }
}
