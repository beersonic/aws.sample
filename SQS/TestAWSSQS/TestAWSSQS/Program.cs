using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAWSSQS
{
    class Program
    {
        const bool useFIFO = true;
        const String SQS_STANDARD = "https://sqs.us-east-1.amazonaws.com/728823697784/QUEUE_TEST1";
        const String SQS_FIFO = "https://sqs.us-east-1.amazonaws.com/728823697784/QUEUE_TEST2.fifo";
        const int SQS_FIFO_NUMBER_OF_GROUP = 1;
        const String SQS_URL = useFIFO ? SQS_FIFO : SQS_STANDARD;
        const int RUN_PERIOD_SECOND = 60;

        static void Main(string[] args)
        {
            try
            {
                // create SQS client
                var sqsConfig = new AmazonSQSConfig();
                sqsConfig.ServiceURL = "https://sqs.us-east-1.amazonaws.com";
                var sqsClient = new AmazonSQSClient(sqsConfig);

                if (useFIFO)
                {
                    Dictionary<String, String> dictSQSAttribute = new Dictionary<string, string>();
                    {
                        dictSQSAttribute.Add("ContentBasedDeduplication", "true");
                    }
                    sqsClient.SetQueueAttributes(SQS_URL, dictSQSAttribute);
                }

                // start loop to send message
                int count = 0;
                DateTime dtStart = DateTime.Now;
                int msgId = 0;
                while ((DateTime.Now - dtStart).TotalSeconds < RUN_PERIOD_SECOND)
                {
                    // create sendMessageRequest message
                    SendMessageRequest sendMessageRequest = new SendMessageRequest();
                    {
                        String id = DateTime.Now.Hour + DateTime.Now.Minute + "." + ++msgId;
                        sendMessageRequest.QueueUrl = SQS_URL;
                        sendMessageRequest.MessageBody = "[" + id + "] Message from C# application at " + DateTime.Now.ToString();

                        if (useFIFO)
                        {
                            sendMessageRequest.MessageGroupId = (msgId % SQS_FIFO_NUMBER_OF_GROUP).ToString();
                        }
                    }
                    
                    // send message
                    sqsClient.SendMessage(sendMessageRequest);

                    Console.WriteLine("Publish SQS message {0}", ++count);
                }
                Console.WriteLine("Publish SQS message done");
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR: " + e.ToString());
            }
        }
    }
}
