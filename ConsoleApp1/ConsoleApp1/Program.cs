using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 8080;
            //клиент - серверное приложение ЧАТ
            IPEndPoint Tcp = new IPEndPoint(IPAddress.Parse(ip), port);

            Socket SocketTcp = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            SocketTcp.Bind(Tcp);
            SocketTcp.Listen(5);
            while (true)
            {
                var Listener = SocketTcp.Accept();
                byte[] buffer = new byte[256];
                int size = 0;
                var data = new StringBuilder();
                do
                {
                    size = Listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer,0,size));
                }
                while (Listener.Available>0);

                Console.WriteLine(data);
                Listener.Shutdown(SocketShutdown.Both);
                Listener.Close();
            }
        }
    }
}
