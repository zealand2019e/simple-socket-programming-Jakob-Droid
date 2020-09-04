using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

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
        public void Start()
        {
            TcpListener serverSide = new TcpListener(IPAddress.Loopback, 7777);
            serverSide.Start();
            TcpClient socket = serverSide.AcceptTcpClient();

            DoClient(socket);
        }
        public void DoClient(TcpClient client)
        {
            using (client)
            {
                Stream ns = client.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);

                string line = sr.ReadLine();
                sw.WriteLine(line);
                sw.AutoFlush = true;

                Console.WriteLine("Server activated");
                Console.WriteLine(line);
            }
        }
    }







}
