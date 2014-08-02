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
            selected_projects.Value = "";

            long convertedApproverId;
            if (Int64.TryParse(Session["userid"].ToString(), out convertedApproverId))
            {
                loadProjects(convertedApproverId);
            }
            
        }

    }

    protected void loadProjects(long UCId)
    {
        ProjectModule projectModule = new ProjectModule();

        //Only get ASSIGNED and TERMINATED projects
        IList<Project> allProjects = projectModule.getProjectsByApproverId(UCId);
        /*IList<Project> assignedAndTerminated = new List<Project>();
        foreach (Project project in allProjects)
        {
            if (project.PROJECT_STATUS == APPLICATION_STATUS.ASSIGNED ||
                project.PROJECT_STATUS == APPLICATION_STATUS.TERMINATED)
                assignedAndTerminated.Add(project);
        }
        */
        Session["projects"] = allProjects; // assignedAndTerminated;
        project_list.DataSource = Session["projects"];
        project_list.DataBind();
        selected_projects.Value = "";
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

    private void reloadProjects()
    {
        int currentPage = project_list.CurrentPageIndex;
        long convertedApproverId;
        if (Int64.TryParse(Session["userid"].ToString(), out convertedApproverId))
        {
            loadProjects(convertedApproverId);
            project_list.CurrentPageIndex = currentPage;
            project_list.DataSource = Session["projects"];
            project_list.DataBind();
        }
        selected_projects.Value = "";
    }

    protected void complete_button_Click(object sender, EventArgs e)
    {
        IList<long> selectedProjects = getSelectedProjects();
        ProjectModule projectModule = new ProjectModule();
        projectModule.changeProjectStatuses(selectedProjects, APPLICATION_STATUS.COMPLETED);

        reloadProjects();

    }

    protected void terminate_button_Click(object sender, EventArgs e)
    {
        IList<long> selectedProjects = getSelectedProjects();
        ProjectModule projectModule = new ProjectModule();
        projectModule.changeProjectStatuses(selectedProjects, APPLICATION_STATUS.TERMINATED);

        reloadProjects();
    }

    private IList<long> getSelectedProjects()
    {
        IList<long> selected = new List<long>();

        string concatenated = selected_projects.Value;
        if (concatenated == null || concatenated.Length <= 0)
            return selected;
        string[] selectedItems = concatenated.Split(',');
        foreach (string selectedItem in selectedItems)
        {
            long convertedApplicationId;
            if (!Int64.TryParse(selectedItem, out convertedApplicationId))
                throw new Exception("System error: Cannot find application ID, please contact administrator.");

            selected.Add(convertedApplicationId);
        }

        return selected;

    }
    protected void assigned_button_Click(object sender, EventArgs e)
    {
        IList<long> selectedProjects = getSelectedProjects();
        ProjectModule projectModule = new ProjectModule();
        projectModule.changeProjectStatuses(selectedProjects, APPLICATION_STATUS.ASSIGNED);

        reloadProjects();
    }
}