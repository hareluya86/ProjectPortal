using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CourseException
/// </summary>
public class CourseException : Exception
{
    public CourseException(string message)
        : base("CourseException" + message)
	{
	}
}