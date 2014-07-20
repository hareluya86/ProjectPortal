using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Team
/// </summary>
public class Team
{
    public virtual Int64 TEAM_ID { get; set; }
    public virtual string TEAM_NAME { get; set; }
    public virtual IList<ProjectAssignment> ASSIGNED_TO_PROJECT { get; set; }
    public virtual IList<TeamAssignment> TEAM_ASSIGNMENT { get; set; }

    public Team()
    {
        TEAM_ASSIGNMENT = new List<TeamAssignment>();
        ASSIGNED_TO_PROJECT = new List<ProjectAssignment>();
    }
}