using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Administrator
/// </summary>
public class Administrator : UserAccount
{
	public Administrator()
	{
        ROLE = "ADMINISTRATOR";
	}
}