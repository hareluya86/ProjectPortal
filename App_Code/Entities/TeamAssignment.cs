using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TeamAssignment
/// </summary>
public class TeamAssignment
{
    public virtual Int64 TEAM_ASSIGNMENT_ID { get; set; }
    public virtual Team TEAM { get; set; }
    public virtual Student STUDENT { get; set; }
    public virtual string ROLE { get; set; }
}