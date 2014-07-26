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
            return "MemberPage/Administrator/AdministratorPage.aspx";
        }
        else if ("STUDENT".Equals(userRole))
        {
            return "MemberPage/Student/StudentPage.aspx";
        }
        else if ("PARTNER".Equals(userRole))
        {
            return "MemberPage/Partner/PartnerPage.aspx";
        }
        else if ("UNITCOORDINATOR".Equals(userRole))
        {
            return "MemberPage/UC/UCPage.aspx";
        }
        else
        {
            return "Default.aspx";//return homepage by default
        }
    }
}