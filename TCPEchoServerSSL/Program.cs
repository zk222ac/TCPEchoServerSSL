using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPEchoServerSSL
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = new TcpListener(ip, 7000);
            serverSocket.Start();
            Console.WriteLine("Server started");
            TcpClient connectionSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine("Server activated");
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns) {AutoFlush = true};// enable automatic flushing
            string message = sr.ReadLine();
            while (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine("Client: " + message);
                var answer = message.ToUpper();
                sw.WriteLine(answer);
                message = sr.ReadLine();
            }
            ns.Close();
            connectionSocket.Close();
            serverSocket.Stop();
        }
    }
}
