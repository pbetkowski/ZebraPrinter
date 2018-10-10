using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintScript.Services
{
    class ZplService
    {

        StringService stringService = new StringService();
        public void Print(string zplString)
        {
             {
 
                string ipAddress = "172.16.1.161";
                int port = 9100;

                try
                {
                    System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                    client.Connect(ipAddress, port);


                    StreamWriter writer = new StreamWriter(client.GetStream());

                    writer.Write(zplString);
                    writer.Flush();

                    writer.Close();
                    client.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadKey();
            }
        }
    
    
    }
}
