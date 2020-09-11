using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EchoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Server server = new Server();

            server.Start();
        }
    }
    public class Server
    {
        int clientsConnected = 0;
        public void Start()
        {
            TcpListener serverSide = new TcpListener(IPAddress.Any, 7777);
            serverSide.Start();
            while (true)
            {
                TcpClient socket = serverSide.AcceptTcpClient();
                Console.WriteLine("Server activated");
                clientsConnected++;
                Task.Run(() => DoClient(socket));
                Console.WriteLine($"Client: {clientsConnected} Connected");
            }
        }
        public void DoClient(TcpClient client)
        {
            using (client)
            {
                while (true)
                {
                    try
                    {
                        Stream ns = client.GetStream();
                        StreamReader sr = new StreamReader(ns);
                        StreamWriter sw = new StreamWriter(ns);
                        string line = sr.ReadLine();
                        sw.WriteLine(line);
                        sw.AutoFlush = true;
                        Console.WriteLine(line);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Connection to the Client ended unexpectedly");
                    }

                    if (client.Connected == false)
                    {
                        Console.WriteLine($"Client: {clientsConnected} left the server");
                        clientsConnected--;
                        client.Dispose();
                        break;
                    }
                }
            }
        }
    }
}
