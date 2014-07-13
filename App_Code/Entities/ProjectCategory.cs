using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectCategory
/// </summary>
public class ProjectCategory
{
    public virtual long PROJECT_CATEGORY_ID { get; set; }
    public virtual Project PROJECT { get; set; }
    public virtual Category CATEGORY { get; set; }
}