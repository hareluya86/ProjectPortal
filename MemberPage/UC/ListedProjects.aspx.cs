
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
            Session["applications"] = null;
            Session["selected_applications"] = null;
            loadProjects();
            project_title.Text = "&nbsp;";
            assign_button.Text = "Approve";
            assign_button.Enabled = false;
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
            Session["projectId"] = projectId; //for later use
            loadProject(convertedProjectid);
            //switchPartner(partner);
            //company_contacts_updatePanel.Update();
            //project_updatePanel.Update();
            //project_categories_panel.Update();
            project_title_panel.Update();
            //numbers_panel.Update();
            //applications_panel.Update();
            //apply_button_panel.Update();
            project_details_panel.Update();
        }
        else
        {
            Messenger.setMessage(apply_project_message, "System error: Cannot find student ID, please contact administrator.", LEVEL.DANGER);
        }
    }

    private void loadProject(long projectId)
    {
        ProjectModule projectModule = new ProjectModule();
        Project project = projectModule.getProjectById(projectId);
        
        project_id.Value = projectId.ToString();
        project_title.Text = project.PROJECT_TITLE;
        company_name.Text = project.PROJECT_OWNER.USERNAME;
        contact_name.Text = project.CONTACT_NAME;
        contact_number.Text = project.CONTACT_NUMBER;
        contact_email.Text = project.CONTACT_EMAIL;
        project_requirements.Text = project.PROJECT_REQUIREMENTS;
        uc_comments.Text = project.UC_REMARKS;
        allocated_slots.Text = project.RECOMMENDED_SIZE.ToString();

        //load categories
        IList<ProjectCategory> projectCategories = project.CATEGORIES;
        IList<Category> categories = new List<Category>();

        foreach (ProjectCategory projectCategory in projectCategories)
        {
            Category c = projectCategory.CATEGORY;
            categories.Add(c);
        }

        //load project applications
        num_applications.Text = project.APPLICATIONS.Count.ToString();
        Session["applications"] = project.APPLICATIONS;
        project_application_list.DataSource = Session["applications"];
        project_application_list.DataBind();

        category_list.DataSource = categories;
        category_list.DataBind();

        //enable Apply button
        assign_button.Enabled = true;

    }

    protected void project_application_list_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        if (sender != null)
        {
            //get those records with check box selected
            IList<long> selected = getSelectedApplications();
            
            project_application_list.CurrentPageIndex = e.NewPageIndex;
            project_application_list.DataSource = Session["applications"];
            project_application_list.DataBind();

            checkSelectedCheckbox(selected, project_application_list, "appId");
        }
    }

    private IList<long> getSelectedApplications()
    {
        IList<long> selected = new List<long>();
        
        string concatenated = selected_applications.Value;
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

    private void checkSelectedCheckbox(IList<long> selectedApplicationIds, DataGrid dataGrid, string checkboxId)
    {
        foreach (DataGridItem di in dataGrid.Items)
        {
            HtmlInputCheckBox chkBx = (HtmlInputCheckBox)di.FindControl(checkboxId);
            long convertedApplicationId;
            if (!Int64.TryParse(chkBx.Value, out convertedApplicationId))
                throw new Exception("System error: Cannot find application ID, please contact administrator.");
            
            if (selectedApplicationIds.Contains(convertedApplicationId))
                chkBx.Checked = true;

        }
    }


    protected void approve_project(object sender, EventArgs e)
    {
        long convertedProjectid;
        long convertedStudentid;
        string projectId = project_id.Value;
        string studentId = Session["userid"].ToString();

        try
        {
            if (Int64.TryParse(projectId, out convertedProjectid) &&
                Int64.TryParse(studentId, out convertedStudentid))
            {
                
            }
            else
            {
                throw new Exception("System error: Cannot find student ID, please contact administrator.");
            }
        }
        catch (Exception ex)
        {
            
            Messenger.setMessage(apply_project_message, ex.Message, LEVEL.DANGER);
        }
        finally
        {
            apply_project_popup.Show(); 
            //apply_popup_panel.Update();
            project_details_panel.Update();
        }
    }

    private ProjectApplication assign_project(long studentId, long projectId)
    {
        ProjectModule projectModule = new ProjectModule();
        ProjectApplication projectApplication = projectModule.ApplyProject(studentId, projectId);

        return projectApplication;
    }

    protected void okButton_Click(object sender, EventArgs e)
    {
        long convertedProjectid;

        if (Int64.TryParse(Session["projectId"].ToString(), out convertedProjectid))
        {
            loadProject(convertedProjectid);
        }
        else
        {
            Messenger.setMessage(apply_project_message, "System error: Cannot find student ID, please contact administrator.", LEVEL.DANGER);
            apply_project_popup.Show();
            project_details_panel.Update();
        }
    }
}