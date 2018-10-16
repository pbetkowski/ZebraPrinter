using PrintScript.Model;
using Sap.Data.Hana;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace PrintScript.Services
{
    public class MainController
    {
        StringService stringService = StringService.GetInstance();
        HanaService hanaService = HanaService.CreateInstance();
        HanaConnection conn = new HanaConnection();
        Label daimlerLabel;
        string singleLabel = "";

        public void ExecuteQuery()
        {
            try
            {   

                conn = HanaService.ConnectToDataBase();
                conn.Open();

                string query = Queries.GetQueueOfLabels;
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

                   
                    singleLabel = stringService.ConvertZplFile(stringService.ReadFile(), daimlerLabel);
                    hanaService.CallUpdateProcedure(conn, int.Parse(daimlerLabel.DocEntry), 0);
                    Console.WriteLine(singleLabel);
                    ZplService.Print(singleLabel, IP.Zpl401);
                }

                if (daimlerLabel != null)
                {
                    hanaService.CallUpdateProcedure(conn, int.Parse(daimlerLabel.DocEntry), 2);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                if (daimlerLabel != null)
                {
                    hanaService.CallUpdateProcedure(conn, int.Parse(daimlerLabel.DocEntry), 3);
                }
            }

            finally
            {
                Console.WriteLine("Zaktualizowano");
                conn.Close();
            }
        }

    }
}
