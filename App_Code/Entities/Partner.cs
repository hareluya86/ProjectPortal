using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Partner
/// </summary>
public class Partner : UserAccount
{
    //public string COMPANY_NAME { get; set; }//Equivalent to USERNAME
    public virtual string COMPANY_REG_NUM { get; set; }
    public virtual string FAX { get; set; }

	public Partner()
	{
        ROLE = "PARTNER";
	}
}