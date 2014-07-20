using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubmitNewProject : BaseMemberPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadCategories();
        }
        else
        {
            if (Session["projectid"] != null)
            {

            }
        }

    }

    protected void loadProject(long projectid)
    {
        ProjectModule projectModule = new ProjectModule();
        Project project = projectModule.getProjectById(projectid);

        project_title.Text = project.PROJECT_TITLE;
        contact_name.Text = project.CONTACT_NAME;
        contact_num.Text = project.CONTACT_NUMBER;
        contact_email.Text = project.CONTACT_EMAIL;
        project_requirements.Text = project.PROJECT_REQUIREMENTS;
        
    }

    protected void loadCategories()
    {
        ProjectModule projectModule = new ProjectModule();
        IList<Category> allCategories = projectModule.getAllCategories(0, 9999);

        category_list.DataSource = allCategories;
        category_list.DataBind();
    }

    protected void SubmitProjectButton_Click(object sender, EventArgs e)
    {
        ProjectModule projectModule = new ProjectModule();
        
        //Get user ID for project owner
        long ownerId;
        if(!Int64.TryParse(Session["userid"].ToString(), out ownerId))
        {
            Messenger.setMessage(error_message,"Error getting user ID, please log out and sign in again, or contact administrator.",LEVEL.DANGER);
            return;
        }

        try
        {
            //Get all chosen category IDs
            IList<Int64> categoryIds = new List<Int64>();
            string collectionCategoryIds = Request.Form["selected"];
            string[] categoryIdsStrings = new string[]{}; //an empty array
            if (collectionCategoryIds != null)
            {
                categoryIdsStrings = collectionCategoryIds.Split(',');
            }
            foreach (string categoryIdsString in categoryIdsStrings)
            {
                long longValue = 0;
                Int64.TryParse(categoryIdsString, out longValue);
                categoryIds.Add(longValue);
            }

            //Load all inputs into a Project object
            Project project = new Project();
            if (Session["projectid"] != null)
            {
                project.PROJECT_ID = Convert.ToInt64(Session["projectid"].ToString());
            }
            project.PROJECT_TITLE = project_title.Text;
            project.CONTACT_NAME = contact_name.Text;
            project.CONTACT_NUMBER = contact_num.Text;
            project.CONTACT_EMAIL = contact_email.Text;
            project.PROJECT_REQUIREMENTS = project_requirements.Text;

            int convertedRecommendedSize;
            if (!Int32.TryParse(recommended_size.Text, out convertedRecommendedSize))
                throw new Exception("Invalid project size.");
            project.RECOMMENDED_SIZE = convertedRecommendedSize;
            project.ALLOCATED_SIZE = convertedRecommendedSize;
            Project newProject = projectModule.submitProject(project, ownerId);
            Session["projectid"] = newProject.PROJECT_ID;
            projectModule.registerProjectCategories(newProject, categoryIds);
            Session["projectid"] = null;
            Messenger.setMessage(error_message, "Project registered successfully. You will receive an email to update you of the status.", LEVEL.SUCCESS);
            clearAllFields();
        }
        catch (ProjectSubmissionException psex)
        {
            Messenger.setMessage(error_message, psex.Message, LEVEL.DANGER);
        }
        catch (InvalidEmailAddressException ieaex)
        {
            Messenger.setMessage(error_message, ieaex.Message, LEVEL.DANGER);
        }
        catch (ProjectCategoryRegistrationException pcrex)
        {
            Messenger.setMessage(error_message, "Project is submitted but categories are not registered: "+pcrex.Message
                +"<br />"
                +"You may still update the project by clicking Update.", LEVEL.DANGER);
        }
        catch (Exception ex)
        {
            Messenger.setMessage(error_message, ex.Message, LEVEL.DANGER);
        }
        finally
        {
            error_modal_control.Show();
            okButton.Text = "Ok";

            //SubmitProjectButton.Text = "Update";
            NewProjectUpdatePanel.Update();
        }
    }

    protected void clearAllFields()
    {
        project_title.Text = null;
        contact_name.Text = null;
        contact_num.Text = null;
        contact_email.Text = null;
        project_requirements.Text = null;
        recommended_size.Text = null;
    }
    protected void ResetButton_Click(object sender, EventArgs e)
    {
        clearAllFields();
    }
}