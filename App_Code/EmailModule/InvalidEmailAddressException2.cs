using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InvalidEmailAddressException
/// </summary>
public class InvalidEmailAddressException2 : Exception
{
	public InvalidEmailAddressException2(string message) : base(message)
	{
	}
}