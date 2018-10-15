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
        ZplService zplService = new ZplService();
        StringService stringService = new StringService();
        Label daimlerLabel;
        string singleLabel = "";

        public void ExecuteQuery()
        {
            try
            {

                conn = HanaService.ConnectToDataBase();
                conn.Open();

                string query = Queries.testQuery;
                HanaCommand command = new HanaCommand(query);
                command.Connection = conn;
                HanaDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    daimlerLabel = new Label();
                    daimlerLabel.Supplier = dr["Supplier"].ToString();
                    daimlerLabel.Odbiorca = dr["Odbiorca"].ToString();
                    daimlerLabel.PartNo = dr["Part no"].ToString();
                    daimlerLabel.Quantity = double.Parse(dr["U_QtyOnLabel"].ToString());
                    daimlerLabel.Street = dr["Street"].ToString();
                    daimlerLabel.Address = dr["Adres odbiorcy"].ToString();
                    daimlerLabel.City = dr["City"].ToString();
                    daimlerLabel.AdviceNote = dr["Advice note"].ToString();
                    daimlerLabel.Description = dr["Description"].ToString();
                    daimlerLabel.Gate = dr["Dock/Gate"].ToString();
                    daimlerLabel.LabelNo = double.Parse(dr["Serien"].ToString());
                    daimlerLabel.Date = dr["Date"].ToString();
                    daimlerLabel.SupplierPartNumber = dr["Supplier part no"].ToString();
                    daimlerLabel.GTL = dr["GTL"].ToString();
                    daimlerLabel.DocEntry = dr["DocEntry"].ToString();


                    if (dr.FieldCount != 0)
                    {
                        Console.WriteLine("Znaleziono dokument: DocEntry: " + daimlerLabel.DocEntry);
                    }



                    singleLabel = stringService.ConvertZplFile(stringService.ReadFile(), daimlerLabel);

                    Console.WriteLine("Usypiam...");
                    Thread.Sleep(2000);
                    new Thread(() =>
                    {
                        Console.WriteLine("Drukuję...");
                        Console.WriteLine("Numer etykiety " + daimlerLabel.LabelNo);
                        //ZplService.Print(singleLabel, IP.Zpl401);
                    }).Start();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                HanaService.CallUpdateProcedure(conn, int.Parse(daimlerLabel.DocEntry), 3);
            }

            finally
            {
                Console.WriteLine("Zaktualizowano");
                //  conn.Close();
            }
        }
    }
}
