using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Student
/// </summary>
public class Student : UserAccount
{
    public virtual string FIRSTNAME { get; set; } //Firstname
    public virtual string LASTNAME { get; set; } //Lastname

    public virtual IList<ProjectApplication> PROJECTS_APPLIED { get; set; }
    public virtual IList<Enrollment> COURSE_ENROLLED { get; set; }
    public virtual IList<TeamAssignment> TEAM_ASSIGNMENT { get; set; }
    public virtual string WRITE_UP { get; set; }

    public Student(){
        ROLE = "STUDENT"; //Workaround for Hibernate's limitation
        PROJECTS_APPLIED = new List<ProjectApplication>();
        COURSE_ENROLLED = new List<Enrollment>();
        TEAM_ASSIGNMENT = new List<TeamAssignment>();
    }

}