using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Enrollment
/// </summary>
public class Enrollment
{
    public virtual Int64 ENROLLMENT_ID { get; set; }
    public virtual Int32 SEMESTER { get; set; }
    public virtual Student STUDENT { get; set; }
    public virtual Course COURSE { get; set; }

	public Enrollment()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}