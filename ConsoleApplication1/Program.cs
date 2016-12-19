using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace Client_Socket
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("That program can transfer small file. I've test up to 850kb file");
                IPHostEntry ipHost = Dns.GetHostEntry("localhost");
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

                // Создаем сокет Tcp/Ip
                Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                string fileName = "input.txt";
                string filePath = "";
                byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);

                byte[] fileData = File.ReadAllBytes(filePath + fileName);
                byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData, 4);
                fileData.CopyTo(clientData, 4 + fileNameByte.Length);

                sListener.Connect(ipEndPoint);
                sListener.Send(clientData);
                Console.WriteLine("File:{0} has been sent.", fileName);
                sListener.Close();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("File Sending fail." + ex.Message);
                Console.ReadKey();
            }

        }
    }
}