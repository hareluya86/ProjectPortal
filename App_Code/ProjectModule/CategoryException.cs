using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CategoryException
/// </summary>
public class CategoryException : Exception
{
    public CategoryException(string message)
        : base("CategoryException"+message)
	{
	}
}