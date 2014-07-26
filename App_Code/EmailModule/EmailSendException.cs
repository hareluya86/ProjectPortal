using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmailSendException
/// </summary>
public class EmailSendException : Exception
{
	public EmailSendException(string message) : base("EmailSendException: "+message)
	{
		//
		// TODO: Add constructor logic here
		//
	}
}