using PrintScript.Model;
using Sap.Data.Hana;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace PrintScript.Services
{
    public class SqlService
    {
        HanaConnection conn = new HanaConnection();
        ZplService ZplService = new ZplService();
        StringService stringService = new StringService();
        List<string> list = new List<string>();

   //     static WaitHandle[] waitHandles = new WaitHandle[]
   //{
   //     new AutoResetEvent(false),
   //     //new AutoResetEvent(false)
   //};

        string DocEntry = "";
        string Supplier = "";
        string Odbiorca = "";
        double Quantity = 0;
        string PartNo = "";
        string Street = "";
        string City = "";
        string Address = "";
        string AdviceNote = "";
        string Description = "";
        string Gate = "";
        double LabelNo = 0;
        string Date = "";
        string SupplierPartNumber = "";
        string GTL = "";
        string x = "";
        List<string> list2 = new List<string>();
        public void ExecuteQuery()
        {
            try
            {

                conn = HanaService.ConnectToDataBase();
                conn.Open();
                
                string Query = Queries.testQuery;
                HanaCommand command = new HanaCommand(Query);
                command.Connection = conn;
                HanaDataReader dr = command.ExecuteReader();
                int i = 1;
                while (dr.Read())
                {
                    list = new List<string>();
                    
                    Supplier = dr["Supplier"].ToString();
                    Odbiorca = dr["Odbiorca"].ToString();
                    PartNo = dr["Part no"].ToString();
                    Quantity = double.Parse(dr["U_QtyOnLabel"].ToString());
                    Street = dr["Street"].ToString();
                    Address = dr["Adres odbiorcy"].ToString();
                    City = dr["City"].ToString();
                    AdviceNote = dr["Advice note"].ToString();
                    Description = dr["Description"].ToString();
                    Gate = dr["Dock/Gate"].ToString();
                    LabelNo = double.Parse(dr["Serien"].ToString());
                    Date = dr["Date"].ToString();
                    SupplierPartNumber = dr["Supplier part no"].ToString();
                    GTL = dr["GTL"].ToString();
                    DocEntry = dr["DocEntry"].ToString();

                   
                    list.Add(Odbiorca);  //1
                    list.Add(AdviceNote); //2
                    list.Add(PartNo); //3
                    list.Add(Quantity.ToString()); //4
                    list.Add(Description);  //5
                    list.Add(Supplier); //6
                    list.Add(SupplierPartNumber); //7
                    list.Add(SupplierPartNumber); //8 do zmiany
                    list.Add(Street); //9
                    list.Add(GTL);   //numeric par 1
                    list.Add(Gate);  //numeric par 2
                    list.Add(LabelNo.ToString()); //numeric par4

                    if (dr.FieldCount != 0)
                    {
                        Console.WriteLine("Znaleziono dokument: DocEntry: " + DocEntry);
                    }


                    
                    x = stringService.ConvertZplFile(stringService.ReadFile(), list);
                    list2.Add(x);
                    //Thread.Sleep(2000);
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(Print), waitHandles[0]);
                    //WaitHandle.WaitAll(waitHandles);

                    //await Print(x);

                    //Thread.CurrentThread.Join(10000);
                    //Console.WriteLine(x);
                    //ZplService.Print(await GetString());
                    Console.WriteLine("Usypiam...");
                    Thread.Sleep(5000);
                    new Thread(() =>
                    {
                        Console.WriteLine("Drukuję...");
                        Console.WriteLine("Numer etykiety " + LabelNo);
                        //ZplService.Print(x);
                    }).Start();
                   
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                HanaService.CallUpdateProcedure(conn, int.Parse(DocEntry), 3);
            }

            finally
            {
                Console.Write("Zaktualizowano");
                //  conn.Close();
            }

            //Console.WriteLine(list2.Count);
            //new Thread(() =>
            //{
            //    foreach (var item in list2)
            //    {
            //        ZplService.Print(item);
            //        Thread.Sleep(5000);
            //    }
            //    HanaService.CallUpdateProcedure(conn, int.Parse(DocEntry), 2);
            //}).Start();
      

        }

        //public void Print(object state)
        //{
        //    AutoResetEvent are = (AutoResetEvent)state;
        //    Console.WriteLine("Wchodzę w wątek");
            

            
        //    //ZplService.Print(x);
        //    are.Set();
        //  //  Console.WriteLine("Usypiam...");
        //    Thread.Sleep(5000);
        //}
    }
}
