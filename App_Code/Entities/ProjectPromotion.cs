using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectPromotion
/// </summary>
public class ProjectPromotion
{
    public virtual Int64 PROMOTION_ID { get; set; }
    public virtual string PROMOTION_TITLE { get; set; }
    public virtual string PROMOTION_WRITEUP { get; set; }
    public virtual string PROMOTION_WEBSITE { get; set; }
    public virtual Int64 PROMOTION_ZIPFILE_ID { get; set; }
    public virtual Int64 PROMOTION_VIDEOFILE_ID { get; set; }

    public virtual Int64 PROMOTER_ID { get; set; }

    public ProjectPromotion()
	{
	}
}