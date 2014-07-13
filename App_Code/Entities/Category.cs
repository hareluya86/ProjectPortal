using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Category
/// </summary>
public class Category
{
    public virtual Int64 CATEGORY_ID { get; set; }
    public virtual string CATEGORY_NAME { get; set; }
    public virtual IList<ProjectCategory> CATEGORIES { get; set; }

    public Category()
    {
        CATEGORIES = new List<ProjectCategory>();
    }
}