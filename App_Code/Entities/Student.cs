﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Student
/// </summary>
public class Student : UserAccount
{
    public virtual string FIRSTNAME { get; set; }

    public Student(){
        ROLE = "STUDENT"; //Workaround for Hibernate's limitation
    }
    
}