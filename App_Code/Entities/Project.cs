using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Project
/// </summary>
public class Project
{
    public virtual long PROJECT_ID { get; set; }
    public virtual string PROJECT_TITLE { get; set; }
    public virtual string CONTACT_NAME { get; set; }
    public virtual string CONTACT_NUMBER { get; set; }
    public virtual string CONTACT_EMAIL { get; set; }

    public virtual string PROJECT_REQUIREMENTS { get; set; }

    public virtual Partner PROJECT_OWNER { get; set; }
    
	public Project()
	{
		
	}


}