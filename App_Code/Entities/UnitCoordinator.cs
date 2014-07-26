using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UnitCoordinator
/// </summary>
public class UnitCoordinator : UserAccount
{
    public virtual string FIRSTNAME { get; set; } //Firstname
    public virtual string LASTNAME { get; set; } //Lastname 


	public UnitCoordinator()
	{
        ROLE = "UNITCOORDINATOR";
	}
}