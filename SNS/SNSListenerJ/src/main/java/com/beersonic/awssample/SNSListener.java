package com.beersonic.awssample;

import com.amazonaws.auth.profile.ProfileCredentialsProvider;
import com.amazonaws.services.sns.*;
import com.amazonaws.services.sns.model.SubscribeRequest;
import com.amazonaws.services.sns.model.SubscribeResult;

public class SNSListener {
    final String SNS_TOPIC = "arn:aws:sns:us-east-1:728823697784:beersonic_sns_test1";
    final String AWS_REGION = "us-east-1";
    final String SQS_ARN = "arn:aws:sqs:us-east-1:728823697784:QUEUE_TEST1";
    void Log(String msg) {
        System.out.println(msg);
    }

    void LogError(String msg) {
        Log("ERROR: " + msg);
    }

    public void StartListener() {
        try {
            
            AmazonSNS snsClient = AmazonSNSClient.builder()
                .withRegion(AWS_REGION)
                .withCredentials(new ProfileCredentialsProvider())
                .build();

            SubscribeRequest subscribeRequest = new SubscribeRequest(SNS_TOPIC, "sqs", SQS_ARN);
            SubscribeResult a = snsClient.subscribe(subscribeRequest);

            Log("SubscribeRequest: " + a.getSdkResponseMetadata().getRequestId());
        }
        catch(Exception e)
        {
            LogError(e.getMessage());
        }
    }
}