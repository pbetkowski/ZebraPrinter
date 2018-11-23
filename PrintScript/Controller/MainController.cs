using PrintScript.Model;
using Sap.Data.Hana;
using System;

namespace PrintScript.Services
{
    public class MainController
    {
        private StringService stringService = StringService.GetInstance();
        private HanaService hanaService = HanaService.CreateInstance();
        private HanaConnection conn = new HanaConnection();
        private Label daimlerLabel;
        private string singleLabel = "";
        private int cnt;

        public void ExecuteQuery()
        {
            try
            {   

                conn = HanaService.ConnectToDataBase();
                conn.Open();
                cnt = 0;

                string query = Queries.GetQueueOfLabels;
                HanaCommand command = new HanaCommand(query);
                command.Connection = conn;
                HanaDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    cnt++;
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
                    daimlerLabel.Los = dr["Los"].ToString();

                    
                    singleLabel = stringService.ConvertZplFile(stringService.ReadFile(), daimlerLabel);
                    
                    Console.WriteLine(singleLabel);
                    ZplService.Print(singleLabel, IP.Zpl401);

                    if (cnt == 1)
                    {
                        hanaService.CallUpdateProcedure(conn, int.Parse(daimlerLabel.DocEntry), 0);
                    }

                    return;
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
