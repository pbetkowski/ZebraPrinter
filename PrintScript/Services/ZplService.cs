using System;
using System.IO;


namespace PrintScript.Services
{
    class ZplService
    {

        private StringService stringService = StringService.GetInstance();
        public static void Print(string zplString, string ip)
        {
            string ipAddress = ip;
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
        }

    }
}
