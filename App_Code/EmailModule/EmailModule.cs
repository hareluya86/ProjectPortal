using System;
using System.Collections.Generic;
using System.IO;
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
    private const string SMTP_USERNAME = "AKIAIZHB5N5CNVHH34AQ";  // Replace with your SMTP username. 
    private const string SMTP_PASSWORD = "AoISPpxqi7MIQ4r96LMdZsmDUxXhPLI6h1/EmqGJzOqH";  // Replace with your SMTP password.

    // Amazon SES SMTP host name. This example uses the us-east-1 region.
    private const string HOST = "email-smtp.us-east-1.amazonaws.com";
    //private const string HOST = "tls://email-smtp.us-east-1.amazonaws.com";

    // Port we will connect to on the Amazon SES SMTP endpoint. We are choosing port 587 because we will use
    // STARTTLS to encrypt the connection.
    private const int PORT = 587;
    //private const int PORT = 465;

    private const string ADMIN_ADDRESS = "hareluya86@hotmail.com";

    private const string ERROR_LOG_DIR = "Logs";
    private const string ERROR_LOG_FILE = "email_error.txt";

	public EmailModule()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void sendEmail(string subject, string body, string recipientAddress)
    {
        EmailModule.validateEmail(recipientAddress);

        //SmtpClient client = new SmtpClient(HOST, PORT);
        
        using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(HOST, PORT))
        {
            client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
            client.EnableSsl = true;
            //client.UseDefaultCredentials = true;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage mail = new MailMessage(ADMIN_ADDRESS, recipientAddress, subject, body);
            mail.IsBodyHtml = true;

            try
            {
                Console.WriteLine("Attempting to send an email through the Amazon SES SMTP interface...");
                client.Send(mail);
                Console.WriteLine("Email sent!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("The email was not sent.");
                Console.WriteLine("Error message: " + ex.Message);
                logError("Email " + subject + " was not sent to " + recipientAddress + ". Error message: " + ex.Message);
                throw new EmailSendException(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
        }
    }

    public void sendEmailToMany(string subject, string body, IList<string> addresses)
    {
        foreach(string address in addresses)
        {
            validateEmail(address);
        }

        //SmtpClient client = new SmtpClient(HOST, PORT);

        using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(HOST, PORT))
        {
            client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
            client.EnableSsl = true;
            //client.UseDefaultCredentials = true;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(ADMIN_ADDRESS);
            mail.Body = body;
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            foreach (string address in addresses)
            {
                mail.To.Add(new MailAddress(address));
            }

            try
            {
                Console.WriteLine("Attempting to send an email through the Amazon SES SMTP interface...");
                client.Send(mail);
                Console.WriteLine("Email sent!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("The email was not sent.");
                Console.WriteLine("Error message: " + ex.Message);
                logError("Multiple email " + subject + " was not sent due to: " + ex.Message);
                throw new EmailSendException(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
        }
    }

    public static void validateEmail(string email)
    {
        if(email == null || email.Length <= 0)
            throw new InvalidEmailAddressException2("Email address is empty");

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
        if (!Directory.Exists(HttpRuntime.AppDomainAppPath + ERROR_LOG_DIR))
            Directory.CreateDirectory(HttpRuntime.AppDomainAppPath + ERROR_LOG_DIR);
        FileStream saveFileStream = File.Create(HttpRuntime.AppDomainAppPath + ERROR_LOG_DIR + ERROR_LOG_FILE);
        using (System.IO.StreamWriter file = File.AppendText(HttpRuntime.AppDomainAppPath +"\\"+ ERROR_LOG_DIR +"\\"+ ERROR_LOG_FILE))
        {
            file.WriteLine(timeStamp + ": " + errorMessage);
        }
    }
}