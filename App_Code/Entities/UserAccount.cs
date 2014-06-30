using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public abstract class UserAccount
{
    
    public virtual Int64 USERID { get; set; } //Unique Identifier
    public virtual string USERNAME { get; set; } //Display name
    public virtual string PASSWORD { get; set; } //Login password
    public virtual string ROLE { get; set; } //User type

}