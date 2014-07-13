using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectApplication
/// </summary>
public class ProjectApplication
{
    public virtual Int64 APPLICATION_ID { get; set; }
    public virtual string APPLICATION_STATUS { get; set; }

    public virtual Student APPLICANT { get; set; }
    public virtual Project PROJECT { get; set; }

	public ProjectApplication()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}