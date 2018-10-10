using PrintScript.Model;
using Sap.Data.Hana;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PrintScript.Services
{
    public class SqlService
    {
        HanaConnection conn = new HanaConnection();
        ZplService ZplService = new ZplService();
        StringService stringService = new StringService();
        List<string> list = new List<string>();

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
                    

                    

                    Console.WriteLine("ETYKIETA" + i);
                    i++;
                    x = stringService.ConvertZplFile(stringService.ReadFile(), list);
                    Console.WriteLine(x);
                    //ZplService.Print(x);

                    new Thread(() =>
                    {
                        ZplService.Print(x);
                    }).Start();

                    if (i == 3)
                    {
                        return;
                    }
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            finally
            {
                conn.Close();
            }

            Console.ReadKey();
        }
    }
}
