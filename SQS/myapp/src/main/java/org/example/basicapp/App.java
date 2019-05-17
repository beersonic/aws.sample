package org.example.basicapp;

import java.util.ArrayList;


/**
 * Hello world!
 *
 */
public class App {
    
    public static void main(String[] args) {
        int NUMBER_OF_THREAD = 10;

        ArrayList<Thread> threads = new ArrayList<Thread>();

        for (int i = 0 ; i < NUMBER_OF_THREAD ; ++i)
        {
            final int x = i;
            Thread t = new Thread(new Runnable(){
                public void run(){
                    SQSReciever recv = new SQSReciever();
                    recv.StartReciever(x);
                }
            });
            t.start();

            threads.add(t);
        }
    }
}
