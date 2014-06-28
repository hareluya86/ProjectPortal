using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoginException
/// </summary>
public class LoginException : Exception
{
    public LoginException(string message): base(message)
    {
        
    }

    public LoginException(string message, Exception inner)
        : base(message,inner)
    {

    }
}