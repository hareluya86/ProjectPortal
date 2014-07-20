
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class PendingProjects : BaseMemberPage
{
    UserModule userModule = new UserModule();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["partnerid"] = null;
            Session["projects"] = null;
            loadPendingProjects();
            //project_title.Text = "&nbsp;";
            approve_button.Text = "Approve";
            approve_button.Enabled = false;
            reject_button.Text = "Reject";
            reject_button.Enabled = false;
        }
    }

    private void loadPendingProjects()
    {
        ProjectModule projectModule = new ProjectModule();
        project_titles.DataSource = projectModule.getAllPendingProjects(0, 9999); //Get it up first before we optimize this

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
            //project_title_panel.Update();
            //numbers_panel.Update();
            //applications_panel.Update();
            //apply_button_panel.Update();
            project_details_panel.Update();
        }
        else
        {
            Messenger.setMessage(approve_project_message, "Cannot find project ID, please contact administrator.", LEVEL.DANGER);
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
        recommended_size.Text = project.RECOMMENDED_SIZE.ToString();
        allocated_size.Text = project.ALLOCATED_SIZE.ToString(); //already set to the same value as recommended size

        //load categories
        IList<ProjectCategory> projectCategories = project.CATEGORIES;
        IList<Category> categories = new List<Category>();

        foreach (ProjectCategory projectCategory in projectCategories)
        {
            Category c = projectCategory.CATEGORY;
            categories.Add(c);
        }

        //load project applications

        category_list.DataSource = categories;
        category_list.DataBind();

        //enable Apply button
        approve_button.Enabled = true;
        reject_button.Enabled = true;
    }

    protected void project_application_list_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        if (sender != null)
        {
            //project_application_list.CurrentPageIndex = e.NewPageIndex;
            //project_application_list.DataSource = Session["projects"];
            //project_application_list.DataBind();
        }
    }

    protected void approve_project(object sender, EventArgs e)
    {
        long convertedProjectid;
        ProjectModule projectModule = new ProjectModule();

        try
        {
            if (!Int64.TryParse(project_id.Value, out convertedProjectid))
                throw new Exception("Cannot find project ID, please contact administrator.");

            Project project = projectModule.getProjectById(convertedProjectid);

            //set all inputted variables into the Project object
            //Only set fields that could be changed in the frontend screen
            project.PROJECT_TITLE = project_title.Text;
            project.CONTACT_NAME = contact_name.Text;
            project.CONTACT_NUMBER = contact_number.Text;
            project.CONTACT_EMAIL = contact_email.Text;
            project.PROJECT_REQUIREMENTS = project_requirements.Text;
            project.UC_REMARKS = uc_comments.Text;

            int convertedAllocatedSize;
            if (!Int32.TryParse(allocated_size.Text, out convertedAllocatedSize))
                throw new Exception("Invalid allocated size entered");

            project.ALLOCATED_SIZE = convertedAllocatedSize;

            int convertedRecommendedSize;
            if (!Int32.TryParse(allocated_size.Text, out convertedRecommendedSize))
                throw new Exception("Invalid recommended size entered");

            project.RECOMMENDED_SIZE = convertedRecommendedSize;

            projectModule.approveProject(project);

            //Success
            Messenger.setMessage(approve_project_message, "Project is approved! An email notification has been sent to the project owner.", LEVEL.SUCCESS);
        }
        catch (Exception ex)
        {
            Messenger.setMessage(approve_project_message, ex.Message, LEVEL.DANGER);
            
        }
        finally
        {
            approve_project_popup.Show();
            project_details_panel.Update();
        }
    }

    private void approve_project(long projectId)
    {

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
            Messenger.setMessage(approve_project_message, "Cannot find student ID, please contact administrator.", LEVEL.DANGER);
            
        }
        //approve_project_popup.Show();
        //project_details_panel.Update();
        //project_list_panel.Update();
        //approve_button_panel.Update();
        //EntireListedProjectsPage.Update();
        Response.Redirect(Request.RawUrl);
    }

    protected void reject_project(object sender, EventArgs e)
    {
        long convertedProjectid;
        ProjectModule projectModule = new ProjectModule();

        try
        {
            if (!Int64.TryParse(project_id.Value, out convertedProjectid))
                throw new Exception("Cannot find project ID, please contact administrator.");

            Project project = projectModule.getProjectById(convertedProjectid);

            //set all inputted variables into the Project object
            //Only set fields that could be changed in the frontend screen
            project.PROJECT_TITLE = project_title.Text;
            project.CONTACT_NAME = contact_name.Text;
            project.CONTACT_NUMBER = contact_number.Text;
            project.CONTACT_EMAIL = contact_email.Text;
            project.PROJECT_REQUIREMENTS = project_requirements.Text;
            project.UC_REMARKS = uc_comments.Text;

            int convertedAllocatedSize;
            if (!Int32.TryParse(allocated_size.Text, out convertedAllocatedSize))
                throw new Exception("Invalid allocated size entered");

            project.ALLOCATED_SIZE = convertedAllocatedSize;

            int convertedRecommendedSize;
            if (!Int32.TryParse(allocated_size.Text, out convertedRecommendedSize))
                throw new Exception("Invalid recommended size entered");

            project.RECOMMENDED_SIZE = convertedRecommendedSize;

            projectModule.rejectProject(project);

            //Success
            Messenger.setMessage(approve_project_message, "Project is rejected! An email notification has been sent to the project owner.", LEVEL.WARNING);
        }
        catch (Exception ex)
        {
            Messenger.setMessage(approve_project_message, ex.Message, LEVEL.DANGER);

        }
        finally
        {
            approve_project_popup.Show();
            project_details_panel.Update();
        }
    }

    private void clearFields()
    {
        
    }
}