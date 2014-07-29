
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ApprovedProjects : BaseMemberPage
{
    UserModule userModule = new UserModule();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["partnerid"] = null;
            Session["applications"] = null;

            long convertedProjectid;
            if (!Int64.TryParse(Session["userid"].ToString(), out convertedProjectid))
                throw new Exception("System error: Cannot find student ID, please contact administrator.");
            loadProjects(convertedProjectid);
            project_title.Text = "&nbsp;";
            assign_button.Text = "Approve";
            assign_button.Enabled = false;
            selected_applications.Value = "";
        }
    }

    private void loadProjects(long approverId)
    {
        ProjectModule projectModule = new ProjectModule();
        project_titles.DataSource = projectModule.getProjectsByApproverId(approverId);

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

        //load project applications (only pending)
        IList<ProjectApplication> pendingApplications = new List<ProjectApplication>();
        foreach (ProjectApplication application in project.APPLICATIONS)
        {
            if (application.APPLICATION_STATUS == APPLICATION_STATUS.PENDING)
                pendingApplications.Add(application);
        }
        num_applications.Text = pendingApplications.Count.ToString();
        Session["applications"] = pendingApplications;
        project_application_list.DataSource = Session["applications"];
        project_application_list.DataBind();

        category_list.DataSource = categories;
        category_list.DataBind();

        //enable Apply button
        assign_button.Enabled = true;

        //If project has already been assigned, show a disabled overlay and the project members
        ProjectAssignment projectAssignment = null;

        //Clear all selected applications from other projects
        selected_applications.Value = ""; 
        if(project.ASSIGNED_TEAMS != null && project.ASSIGNED_TEAMS.Count > 0)
            projectAssignment = project.ASSIGNED_TEAMS.First(); //Because it is many-to-many, we use only the first result and assume that it will always have 1 team

        if (projectAssignment != null)
        {
            assigned_project_panel.Visible = true;
            IList<Student> projectMembers = new List<Student>();
            Team assignedTeam = projectAssignment.TEAM;

            foreach (TeamAssignment assignment in assignedTeam.TEAM_ASSIGNMENT)
            {
                Student member = assignment.STUDENT;
                projectMembers.Add(member);
            }
            assigned_project_members.DataSource = projectMembers;
            assigned_project_members.DataBind();

            assign_button.Enabled = false;
        }
        else
        {
            assigned_project_panel.Visible = false;
        }

        

        
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


    protected void assign_project(object sender, EventArgs e)
    {
        long convertedProjectid;
        string projectId = project_id.Value;
        IList<long> selectedApplications = getSelectedApplications();
        Team newTeam;

        try
        {
            //if(selectedApplications.Count <= 0)
                //throw new Exception("System error: Cannot find student ID, please contact administrator.");
            if (!Int64.TryParse(projectId, out convertedProjectid))
                throw new Exception("System error: Cannot find student ID, please contact administrator.");
            
            newTeam = this.assign_project(convertedProjectid, selectedApplications, true);
            Messenger.setMessage(apply_project_message, "Project has been assigned to team "+newTeam.TEAM_ID+" and email has been sent to the team members and project owner.", LEVEL.SUCCESS);
        }
        catch (ProjectAssignmentException paex)
        {
            Messenger.setMessage(apply_project_message, paex.Message, LEVEL.DANGER);
        }
        catch (EmailSendException esex)
        {
            Messenger.setMessage(apply_project_message, "Project is approved but email is not sent successfully: " + esex.Message, LEVEL.WARNING);
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

    private Team assign_project(long projectId, IList<long> applicationIds, bool rejectOthers)
    {
        ProjectModule projectModule = new ProjectModule();
        Team newTeam = projectModule.assignProject(projectId, applicationIds, rejectOthers);

        return newTeam;
    }

    protected void okButton_Click(object sender, EventArgs e)
    {
        long convertedProjectid;

        if (Int64.TryParse(Session["projectId"].ToString(), out convertedProjectid))
        {
            loadProject(convertedProjectid);
            //loadProjects();

        }
        else
        {
            Messenger.setMessage(apply_project_message, "System error: Cannot find student ID, please contact administrator.", LEVEL.DANGER);
            apply_project_popup.Show();
            project_details_panel.Update();
        }
    }
}