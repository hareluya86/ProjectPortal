using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectValidationException
/// </summary>
public class ProjectValidationException : Exception
{
	public ProjectValidationException(string message) : base("ProjectValidationException: "+message)
	{
		
	}
}