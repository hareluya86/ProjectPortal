using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for EmailModule
/// </summary>
public class EmailModule
{
    // Supply your SMTP credentials below. Note that your SMTP credentials are different from your AWS credentials.
    private const string SMTP_USERNAME = "";  // Replace with your SMTP username. 
    private const string SMTP_PASSWORD = "";  // Replace with your SMTP password.

    // Amazon SES SMTP host name. This example uses the us-east-1 region.
    private const string HOST = "email-smtp.us-east-1.amazonaws.com";

    // Port we will connect to on the Amazon SES SMTP endpoint. We are choosing port 587 because we will use
    // STARTTLS to encrypt the connection.
    private const int PORT = 587;

    private const string ADMIN_ADDRESS = "hareluya86@hotmail.com";

    private const string ERROR_LOG_PATH = "Logs\\email_error.txt";

	public EmailModule()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void sendEmail(string subject, string body, string recipientAddress)
    {
        EmailModule.validateEmail(recipientAddress);

        SmtpClient client = new SmtpClient(HOST, PORT);
        client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
        client.EnableSsl = true;
        try
        {
            Console.WriteLine("Attempting to send an email through the Amazon SES SMTP interface...");
            client.Send(ADMIN_ADDRESS, recipientAddress, subject, body);
            Console.WriteLine("Email sent!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("The email was not sent.");
            Console.WriteLine("Error message: " + ex.Message);
            logError("Email "+subject+" was not sent to "+recipientAddress+". Error message: "+ex.Message);
        }

    }

    public static void validateEmail(string email)
    {
        string[] checkAt = email.Split('@');
        if (checkAt.Length < 2)
            throw new InvalidEmailAddressException2("Email address does not contain @");

        if (checkAt.Length > 2)
            throw new InvalidEmailAddressException2("Email address contains more than one @");

        if (email.Contains('/') || email.Contains('\\') || email.Contains('*') || email.Contains('\''))
            throw new InvalidEmailAddressException2("Email address contains illegal characters.");
    }

    private void logError(string errorMessage)
    {
        String timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        System.IO.StreamWriter file = new System.IO.StreamWriter(ERROR_LOG_PATH, true);
        file.WriteLine(timeStamp + ": " + errorMessage);

        file.Close();
    }
}