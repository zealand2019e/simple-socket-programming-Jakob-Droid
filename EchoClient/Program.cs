﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace EchoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Client client = new Client();
            client.Start();
        }        
    }

    public class Client
    {
        public void Start()
        {
            TcpClient socket = new TcpClient("localhost", 7);
            using (socket)
            {
                Stream ns = socket.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine("Eg Peter");
                string line = Console.ReadLine();
                sw.WriteLine(line);
                sw.Flush();

            }

        }
    }
}
