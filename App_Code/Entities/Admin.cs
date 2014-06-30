using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Administrator
/// </summary>
public class Admin : UserAccount
{
    public virtual string FAX { get; set; }

	public Admin()
	{
        ROLE = "ADMINISTRATOR";
	}
}