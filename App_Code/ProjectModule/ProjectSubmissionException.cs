using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectSubmissionException
/// </summary>
public class ProjectSubmissionException : Exception
{
	public ProjectSubmissionException(string message) : base(message)
	{
	}
}