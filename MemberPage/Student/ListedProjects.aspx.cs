using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ListedProjects : BaseMemberPage
{
    UserModule userModule = new UserModule();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["partnerid"] = null;
            loadProjects();
            project_title.Text = "&nbsp;";

        }
    }

    private void loadProjects()
    {
        ProjectModule projectModule = new ProjectModule();
        project_titles.DataSource = projectModule.getAllOpenedProjects(0, 9999); //Get it up first before we optimize this

        project_titles.DataBind();
    }

    protected void loadProject(object sender, EventArgs e)
    {
        Button projectButton = (Button)sender;
        string projectId = projectButton.CommandArgument;

        long convertedProjectid;

        if (Int64.TryParse(projectId, out convertedProjectid))
        {
            loadProject(convertedProjectid);
            //switchPartner(partner);
            //company_contacts_updatePanel.Update();
            project_updatePanel.Update();
            project_categories_panel.Update();
            project_title_panel.Update();
        }
    }

    private void loadProject(long projectId)
    {
        ProjectModule projectModule = new ProjectModule();
        Project project = projectModule.getProjectById(projectId);

        project_title.Text = project.PROJECT_TITLE;
        company_name.Text = project.PROJECT_OWNER.USERNAME;
        contact_name.Text = project.CONTACT_NAME;
        project_requirements.Text = project.PROJECT_REQUIREMENTS;
        uc_comments.Text = project.UC_REMARKS;

        //load categories
        IList<ProjectCategory> projectCategories = project.CATEGORIES;
        IList<Category> categories = new List<Category>();

        foreach (ProjectCategory projectCategory in projectCategories)
        {
            Category c = projectCategory.CATEGORY;
            categories.Add(c);
        }

        category_list.DataSource = categories;
        category_list.DataBind();

    }
}