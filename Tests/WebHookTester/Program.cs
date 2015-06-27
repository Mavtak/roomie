using System;
using WebCommunicator;

namespace Roomie.Tests.WebHookTester
{
    class Program
    {
        static string host = "localhost:57680";
        //static string host = "beta.roomiebot.com";

        static void Main(string[] args)
        {
            TestDreamingnestMessage(new Message(System.IO.File.Open("SyncWithCloud reuest.xml", System.IO.FileMode.Open)));
            


            Console.ReadKey();
        }

        static void TestDreamingnestPing()
        {
            var client = new CommunicatorClient("http://" + host + "/communicator/", "4241084ebc474b35", "916fa93f32134cf2");

            var response = client.PingServer();
            Console.WriteLine(response);
        }

        static void TestDreamingnestMessage(Message message)
        {
            TestMessage
                (
                message: message,
                address: "http://" + host + "/communicator/",
                accessKey: "4241084ebc474b35",
                encryptionKey: "916fa93f32134cf2"
                );
        }

        static void TestMessage(Message message, string address, string accessKey, string encryptionKey = null)
        {
            WriteDivider();
            Console.WriteLine(message);
            WriteDivider();

            var client = new CommunicatorClient
            (
                url: address,
                accessKey: accessKey,
                encryptionKey: encryptionKey
            );

            Console.WriteLine("Sending message...");

            var response = client.SendMessage(message, encryptionKey != null, 1);

            Console.WriteLine(response.ToString());

            WriteDivider();
        }


        static void WriteDivider()
        {
            Console.WriteLine("================================================================================");
        }
    }
}
