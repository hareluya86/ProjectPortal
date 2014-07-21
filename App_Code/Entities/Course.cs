using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Course
/// </summary>
public class Course 
{
    public virtual Int64 COURSE_ID { get; set;}
    public virtual string COURSE_NAME { get; set; }

    public virtual IList<Enrollment> ENROLLMENTS { get; set; }

	public Course()
	{
        ENROLLMENTS = new List<Enrollment>();
	}
}