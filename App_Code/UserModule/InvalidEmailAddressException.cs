using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InvalidEmailAddressException
/// </summary>
public class InvalidEmailAddressException : Exception
{
	public InvalidEmailAddressException(string message) : base(message)
	{
		//
		// TODO: Add constructor logic here
		//
	}
}