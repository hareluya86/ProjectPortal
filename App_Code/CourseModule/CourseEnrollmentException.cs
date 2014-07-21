using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CourseEnrollmentException
/// </summary>
public class CourseEnrollmentException : Exception
{
	public CourseEnrollmentException(string message) : base("CourseEnrollmentException: "+message)
	{
	}
}