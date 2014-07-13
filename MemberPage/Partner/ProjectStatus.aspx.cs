using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProjectStatus : BaseMemberPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //clear all page variables
            Session["projects"] = null;

            long ownderid;
            if (Int64.TryParse(Session["userid"].ToString(), out ownderid))
            {
                loadProjects(ownderid);
            }
            
        }

    }

    protected void loadProjects(long ownerId)
    {
        ProjectModule projectModule = new ProjectModule();
         
        Session["projects"] = projectModule.getProjectsByOwnerId(ownerId);
        project_list.DataSource = Session["projects"];
        project_list.DataBind();
    }

    protected void project_list_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        if (sender != null)
        {
            project_list.CurrentPageIndex = e.NewPageIndex;
            project_list.DataSource = Session["projects"];
            project_list.DataBind();
        }
    }
}