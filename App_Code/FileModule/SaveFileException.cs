using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SaveFileException
/// </summary>
public class SaveFileException : Exception
{
    public SaveFileException(string message)
        : base("SaveFileException: "+message)
	{
	}
}