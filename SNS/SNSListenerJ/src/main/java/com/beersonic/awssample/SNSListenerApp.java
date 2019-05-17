package com.beersonic.awssample;

/**
 * Hello world!
 *
 */
public class SNSListenerApp 
{
    public static void main( String[] args )
    {
        try{
            SNSListener listener = new SNSListener();
            listener.StartListener();
        }
        catch(Exception e)
        {
            System.out.println("ERROR: " + e.getMessage() + "\n" + e.toString());
        }
    }
}
