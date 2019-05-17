using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.Runtime;

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
        static void Main(string[] args)
        {
            try
            {
                AWSConfigs config = new AWSConfigs();
                {
                    
                }
                AWSCredentials credential = new StoredProfileAWSCredentials("default");
                var snsClient = new AmazonSimpleNotificationServiceClient(credential);

                String msg = "This message is from C# application";
                PublishRequest req = new PublishRequest("arn:aws:sns:us-east-1:728823697784:beersonic_sns_test1", msg);
                PublishResponse resp = snsClient.Publish(req);

                Console.WriteLine("MessageId: " + resp.MessageId);
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }
    }
}
