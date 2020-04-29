using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using EasyTcp3.ClientUtils;

namespace EasyTcp3.Examples.SpeedTest
{
    public static class SpeedTestClient
    {
        const int Port = 5_001;
        const int MessageCount = 100_000;
        const string Message = "Message";
        
        public static void RunSpeedTest()
        {
            var client = new EasyTcpClient();
            if (!client.Connect(IPAddress.Loopback, Port)) return;

            byte[] message = Encoding.UTF8.GetBytes(Message);
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int x = 0; x < MessageCount; x++) client.SendAndGetReply(message, TimeSpan.FromSeconds(1)); 

            sw.Stop();
            Console.WriteLine($"ElapsedMilliseconds SpeedTest: {sw.ElapsedMilliseconds}");
            Console.WriteLine($"Average SpeedTest: {sw.ElapsedMilliseconds / (double)MessageCount}");
        }
    }
}