using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectAssignment
/// </summary>
public class ProjectAssignment
{
    public virtual Int64 PROJECT_ASSIGNMENT_ID { get; set; }
    public virtual Team TEAM { get; set; }
    public virtual Project PROJECT { get; set; }
}