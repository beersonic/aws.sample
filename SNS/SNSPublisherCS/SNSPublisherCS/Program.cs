using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SNSPublisherCS
{
    class Program
    {
        // config
        const String SNS_TOPIC = "arn:aws:sns:us-east-1:728823697784:beersonic_sns_test1";

        // variable
        static AmazonSimpleNotificationServiceClient _snsClient = null;
        
        static void Main(string[] args)
        {
            try
            {
                StartSNSClient();

                SendSNSMessages(10, 1);
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }

        static void StartSNSClient()
        {
            Console.WriteLine("Starting SNSClient");
            _snsClient = new AmazonSimpleNotificationServiceClient();
            Console.WriteLine("Starting SNSClient done");
        }

        static void SendSNSMessages(int numberOfMsg, double intervalBetweenMsg)
        {
            for (int i = 0; i < numberOfMsg; ++i)
            {
                String msg = String.Format("This SNS message [{0}] from C# application", i);
                PublishRequest req = new PublishRequest(SNS_TOPIC, msg);
                PublishResponse resp = _snsClient.Publish(req);

                Console.WriteLine("Message is sent, MessageId: " + resp.MessageId);

                Thread.Sleep((int)(intervalBetweenMsg * 1000));
            }
        }
    }
}
