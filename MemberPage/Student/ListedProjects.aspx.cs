
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
            loadProjects();
            project_title.Text = "&nbsp;";
            apply_button.Text = "Apply";
            apply_button.Enabled = false;
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
            Messenger.setMessage(apply_project_message, "Cannot find student ID, please contact administrator.", LEVEL.DANGER);
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
        apply_button.Enabled = true;

        //If project has already been assigned, show a disabled overlay and the project members
        ProjectAssignment projectAssignment = null;

        if (project.ASSIGNED_TEAMS != null && project.ASSIGNED_TEAMS.Count > 0)
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

            apply_button.Enabled = false;
        }
        else
        {
            assigned_project_panel.Visible = false;
        }

        //get project documents
        FileModule fileModule = new FileModule();
        ProjectDocument projectDocument = fileModule.getProjectDocumentByProjectId(project.PROJECT_ID);
        if (projectDocument != null)
        {
            project_document_link.NavigateUrl = "#";
            string escapedPath = projectDocument.PROJECTFILE_PATH.Replace("\\","/");
            project_document_link.Attributes.Add("onclick", "window.open(\"../../" + escapedPath
                                               + "\",\"_blank\",\"menubar=no,height=600,width=800\");");
            project_document.Text = projectDocument.PROJECTFILE_NAME;
            project_document.Visible = true;
        }
        else
        {
            project_document_link.Attributes.Clear();
            project_document.Visible = false;
        }
        


    }

    protected void project_application_list_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        if (sender != null)
        {
            project_application_list.CurrentPageIndex = e.NewPageIndex;
            project_application_list.DataSource = Session["applications"];
            project_application_list.DataBind();
        }
    }

    protected void apply_project(object sender, EventArgs e)
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
                ProjectApplication projectApplication = applyProject(convertedStudentid, convertedProjectid);
                Messenger.setMessage(apply_project_message, "You have applied for project "
                + projectApplication.PROJECT.PROJECT_TITLE + ". You will be notified by email of the status of your"
                + " application. Please make sure your email address is valid.", LEVEL.SUCCESS);
            }
            else
            {
                throw new Exception("Cannot find student ID, please contact administrator.");
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

    private ProjectApplication applyProject(long studentId, long projectId)
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
            Messenger.setMessage(apply_project_message, "Cannot find student ID, please contact administrator.", LEVEL.DANGER);
            apply_project_popup.Show();
            project_details_panel.Update();
        }
    }
}