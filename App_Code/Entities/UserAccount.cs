using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public abstract class UserAccount
{
    
    public virtual Int64 USERID { get; set; }
    public virtual string PASSWORD { get; set; }
    public virtual string ROLE { get; set; }

}