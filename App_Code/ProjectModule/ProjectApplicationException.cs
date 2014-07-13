using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectApplicationException
/// </summary>
public class ProjectApplicationException : Exception
{
	public ProjectApplicationException(string message) : base(message)
	{
	}
}