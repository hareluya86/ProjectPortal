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
    public virtual string PROJECT_STATUS { get; set; }

    public virtual string UC_REMARKS { get; set; }
    public virtual string PROJECT_REQUIREMENTS { get; set; }
    public virtual int RECOMMENDED_SIZE { get; set; }
    public virtual int ALLOCATED_SIZE { get; set; }

    public virtual Partner PROJECT_OWNER { get; set; }
    public virtual IList<ProjectApplication> APPLICATIONS { get; set; }
    public virtual IList<ProjectCategory> CATEGORIES { get; set; }
    public virtual IList<ProjectAssignment> ASSIGNED_TEAMS { get; set; }

    public Project()
    {
        APPLICATIONS = new List<ProjectApplication>();
        CATEGORIES = new List<ProjectCategory>();
        ASSIGNED_TEAMS = new List<ProjectAssignment>();
    }
}