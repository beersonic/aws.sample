package org.example.basicapp;

import java.util.List;
import java.util.Random;

import com.amazonaws.auth.profile.ProfileCredentialsProvider;
import com.amazonaws.services.sqs.AmazonSQS;
import com.amazonaws.services.sqs.AmazonSQSClient;
import com.amazonaws.services.sqs.model.Message;

public class SQSReciever
{
    String _name = "";

    final String SQS_STANDARD = "https://sqs.us-east-1.amazonaws.com/728823697784/QUEUE_TEST1";
    final String SQS_FIFO = "https://sqs.us-east-1.amazonaws.com/728823697784/QUEUE_TEST2.fifo";
    //final String SQS_URL = SQS_STANDARD;
    final String SQS_URL = SQS_FIFO;
    final String AWS_REGION = "us-east-1";


    void Log(String msg)
    {
        System.out.println("[" + _name + "] " + msg);
    }
    public void StartReciever(int id)
    {
        _name = "RECV-" + id;
        Log("Start recieving msg from SQS, URL=" + SQS_URL);
        try {            
            AmazonSQS sqs = AmazonSQSClient.builder()
                .withRegion(AWS_REGION)
                .withCredentials(new ProfileCredentialsProvider())
                .build();

            
            Random rand = new Random();
            while(true)
            {
                List<Message> messages = sqs.receiveMessage(SQS_URL).getMessages();
                if (messages.size() > 0)
                {
                    //Log("Receive " + messages.size() + " messages");
                    for (Message msg : messages)
                    {
                        sqs.deleteMessage(SQS_URL, msg.getReceiptHandle());
                        Log("MSG: " + msg.getBody());

                        // fake processing time
                        Thread.sleep(rand.nextInt(200));
                    }
                }
                Thread.sleep(200);
            }
        }catch(Exception e)
        {
            Log("ERROR: " + e.getMessage() + "\n---CALL STACK---\n" + e.toString());
        }
    }
}