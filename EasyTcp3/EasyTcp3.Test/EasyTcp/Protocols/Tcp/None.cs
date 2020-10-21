using EasyTcp3.ClientUtils;
using EasyTcp3.Protocols.Tcp;
using NUnit.Framework;
using EasyTcp3.ServerUtils;

namespace EasyTcp3.Test.EasyTcp.Protocols.Tcp
{
    /// <summary>
    /// Tests for NoneProtocol
    /// </summary>
    public class None
    {
        [Test]
        public void TestReceivingAndSendingData()
        {
            ushort port = TestHelper.GetPort(); 
            var protocol = new PlainTcpProtocol();
            using var server = new EasyTcpServer(protocol).Start(port);
            server.OnDataReceive += (sender, message) => message.Client.Send(message);
            
            using var client = new EasyTcpClient(protocol);
            Assert.IsTrue(client.Connect("127.0.0.1",port));
            
            var data = "testMessage";
            var reply = client.SendAndGetReply(data);
            Assert.IsNotNull(reply);
            Assert.AreEqual(data,reply.ToString());
        }
        
        [Test]
        public void TestSplittingData()
        {
            ushort port = TestHelper.GetPort(); 
            using var server = new EasyTcpServer(new PlainTcpProtocol()).Start(port);
            server.OnDataReceive += (sender, message) => message.Client.Send(message);
            
            using var client = new EasyTcpClient(new PlainTcpProtocol(4));
            Assert.IsTrue(client.Connect("127.0.0.1",port));
            
            var data = "testMessage";
            var reply = client.SendAndGetReply(data);
            Assert.IsNotNull(reply);
            Assert.AreEqual("test",reply.ToString());
        }
    }
}