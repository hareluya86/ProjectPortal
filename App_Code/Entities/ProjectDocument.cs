using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectDocument
/// </summary>
public class ProjectDocument
{
    public virtual Int64 PROJECTFILE_ID { get; set; }
    public virtual string PROJECTFILE_NAME { get; set; }
    public virtual string PROJECTFILE_TYPE { get; set; }
    public virtual string PROJECTFILE_PATH { get; set; }

    public virtual Int64 PROJECTFILE_OWNER { get; set; }

	public ProjectDocument()
	{
		
	}
}