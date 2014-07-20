using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectAssignmentException
/// </summary>
public class ProjectAssignmentException : Exception
{
    public ProjectAssignmentException(string message)
        : base("ProjectAssignmentException: "+message)
	{
	}
}