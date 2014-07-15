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
            Session["projectApplications"] = null;

            long studentid;
            if (Int64.TryParse(Session["userid"].ToString(), out studentid))
            {
                loadProjectApplications(studentid);
            }
            
        }

    }

    protected void loadProjectApplications(long studentId)
    {
        ProjectModule projectModule = new ProjectModule();
         
        Session["projectApplications"] = projectModule.getProjectApplicationsByStudentId(studentId);
        project_application_list.DataSource = Session["projectApplications"];
        project_application_list.DataBind();
    }

    protected void project_application_list_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        if (sender != null)
        {
            project_application_list.CurrentPageIndex = e.NewPageIndex;
            project_application_list.DataSource = Session["projects"];
            project_application_list.DataBind();
        }
    }
}