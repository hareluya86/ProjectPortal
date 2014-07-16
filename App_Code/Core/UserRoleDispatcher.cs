using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserRoleDispatcher
/// </summary>
public class UserRoleDispatcher
{
	public UserRoleDispatcher()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string getPageByUserRole(string userRole)
    {
        
        if ("ADMINISTRATOR".Equals(userRole))
        {
            return "MemberPage/Administrator/Administrator.aspx";
        }
        else if ("STUDENT".Equals(userRole))
        {
            return "MemberPage/Student/Student.aspx";
        }
        else if ("PARTNER".Equals(userRole))
        {
            return "MemberPage/Partner/Partner.aspx";
        }
        else
        {
            return "Default.aspx";//return homepage by default
        }
    }
}