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
    public virtual string EMAIL { get; set; } //Email
    public virtual string PHONE { get; set; } //Phone number

    public virtual string ADDRESS1 { get; set; }
    public virtual string ADDRESS2 { get; set; }
    public virtual string CITY_TOWN { get; set; }
    public virtual string STATE { get; set; }
    public virtual string ZIP_CODE { get; set; }
    public virtual string COUNTRY { get; set; }
}