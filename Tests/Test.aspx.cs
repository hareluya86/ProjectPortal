using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tests_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //loadCourses();
            
        }
    }

    protected void Generate_Schema(object sender, EventArgs e)
    {
        
        TestHibernateClass test = new TestHibernateClass();
        test.generate_schema();
        GenerateSchemaResult.Text = "Passed";
        GenerateSchemaResult.CssClass = "alert-success";
    }

    protected void Test_Insert_UserAccount(object sender, EventArgs e)
    {
        TestUserModule test = new TestUserModule();
        Session["userList"] = test.insertUserAccount(Convert.ToInt32(TestInsertUserAccount_NumberOfUsers_Textbox.Text));

        InsertedUserTable.DataSource = Session["userList"];
        InsertedUserTable.DataBind();
    }

    protected void InsertedUserTable_PageIndexChanging(object sender, DataGridPageChangedEventArgs  e)
    {
        if (sender != null)
        {
            InsertedUserTable.CurrentPageIndex = e.NewPageIndex;
            InsertedUserTable.DataSource = Session["userList"];
            InsertedUserTable.DataBind();
        }
    }

    protected void EncryptPassword(object sender, EventArgs e)
    {
        TestUserModule test = new TestUserModule();
        string rawPassword = password.Text;
        string hash = test.encodePassword(rawPassword);
        encryptedPassword.Text = hash;

        //tells user if the pass matches the hash
        encryptedPassword.Text += "<br/>";
        if (test.checkEncryptedPassword(rawPassword, hash))
        {
            encryptedPassword.Text += "The password "+rawPassword+" and hash "+hash+" matches.";
        }
        else
        {
            encryptedPassword.Text += "The password " + rawPassword + " and hash " + hash + " doesn't match.";
        }
    }

    protected void TestLogin(object sender, EventArgs e)
    {
        TestUserModule test = new TestUserModule();
        
        try
        {
            test.login(input_userid.Text, input_password.Text);
            login_message.Controls.Add(new LiteralControl(
                "<div class='alert alert-success col-sm-10 col-sm-offset-1'>"
                    + "Login successful!"
                    + "</div>"));
        }
        catch (LoginException lex)
        {
            login_message.Controls.Add(new LiteralControl(
                "<div class='alert alert-danger col-sm-10 col-sm-offset-1'>"
                    + lex.Message
                    + "</div>"));
        }
        catch (Exception ex)
        {
            login_message.Controls.Add(new LiteralControl(
                "<div class='alert alert-danger col-sm-10 col-sm-offset-1'>"
                    + ex.Message
                    + "</div>"));
        }
    }

    protected void ClearLogin(object sender, EventArgs e)
    {
        login_message.Controls.Clear();
        input_userid.Text = "";
        input_password.Text = "";
    }

    protected void TestCreateProjects(object sender, EventArgs e)
    {
        ProjectModule projectModule = new ProjectModule();

        long partnerIdLong = Convert.ToInt64(partnerIdTextbox.Text);
        int numProjectsInt = Convert.ToInt32(numProjectsTextbox.Text);
        IList<Project> projects = projectModule.createRandomProjects(partnerIdLong, numProjectsInt);

        Session["projects"] = projects;
        CreatedProjectsTable.DataSource = Session["projects"];
        CreatedProjectsTable.DataBind();
    }

    protected void CreatedProjectsTable_PageIndexChanging(object sender, DataGridPageChangedEventArgs e)
    {
        if (sender != null)
        {
            CreatedProjectsTable.CurrentPageIndex = e.NewPageIndex;
            CreatedProjectsTable.DataSource = Session["projects"];
            CreatedProjectsTable.DataBind();
        }
    }

    protected void TestApplyProject(object sender, EventArgs e)
    {
        long studentId;
        long projectId;
        Int64.TryParse(StudentIdInput.Text,out studentId);
        Int64.TryParse(ProjectIdInput.Text,out projectId);

        ProjectModule projectModule = new ProjectModule();
        try
        {
            ProjectApplication appl = projectModule.ApplyProject(studentId, projectId);
            Messenger.setMessage(ApplyProjectResults, 
                "Application "+appl.APPLICATION_ID+" has been created by student "
                    +studentId+" for project "+projectId+".", 
                LEVEL.SUCCESS);
        }
        catch (ProjectApplicationException paex)
        {
            Messenger.setMessage(ApplyProjectResults, paex.Message, LEVEL.DANGER);
        }
        catch (Exception ex)
        {
            Messenger.setMessage(ApplyProjectResults, ex.Message, LEVEL.DANGER);
        }


    }
    protected void CreateCategoriesButton_Click(object sender, EventArgs e)
    {
        string stringOfCategories = CategoriesInput.Text;
        ProjectModule projectModule = new ProjectModule();

        try
        {
            IList<Category> results = projectModule.createCategories(stringOfCategories);

            Session["categories"] = results;
            AddedCategoriesTable.DataSource = Session["categories"];
            AddedCategoriesTable.DataBind();

            Messenger.setMessage(AddCategoriesMessageBox, "Categories created successfully!", LEVEL.SUCCESS);
        }
        catch (CategoryException catex)
        {
            Messenger.setMessage(AddCategoriesMessageBox, catex.Message, LEVEL.DANGER);
        }
    }
    protected void AddedCategories_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        if (sender != null)
        {
            AddedCategoriesTable.CurrentPageIndex = e.NewPageIndex;
            AddedCategoriesTable.DataSource = Session["categories"];
            AddedCategoriesTable.DataBind();
        }
    }

    protected void CreateCoursesButton_Click(object sender, EventArgs e)
    {
        string stringOfCourses = Courses.Text;
        CourseModule courseModule = new CourseModule();

        try
        {
            long convertedUCId;
            if(!Int64.TryParse(uc_id.Text,out convertedUCId))
                throw new Exception("System error: Cannot find UC ID, please contact administrator.");

            IList<Course> results = courseModule.createCourses(stringOfCourses, convertedUCId);

            Session["courses"] = results;
            AddedCoursesTable.DataSource = Session["courses"];
            AddedCoursesTable.DataBind();

            Messenger.setMessage(AddedCoursesMessage, "Courses created successfully!", LEVEL.SUCCESS);
        }
        catch (CategoryException catex)
        {
            Messenger.setMessage(AddedCoursesMessage, catex.Message, LEVEL.DANGER);
        }
    }


    //Course Module

    private void loadCourses()
    {
        CourseModule courseModule = new CourseModule();
        IList<Course> courses = courseModule.getCourses(0, 9999);

        course_list.DataSource = courses;
        course_list.DataBind();
    }

    private IList<long> getSelectedApplications()
    {
        IList<long> selected = new List<long>();

        string concatenated = selected_course.Value;
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

    protected void course_list_PageIndexChanged(object sender, DataGridPageChangedEventArgs e)
    {
        if (sender != null)
        {
            CourseModule courseModule = new CourseModule();
            IList<Course> courses = courseModule.getCourses(0, 9999);

            course_list.CurrentPageIndex = e.NewPageIndex;
            course_list.DataSource = courses;
            course_list.DataBind();
        }
    }

    protected void enroll_course_Click(object sender, EventArgs e)
    {
        long convertedStudentId;
        string studentId = student_id.Text;
        IList<long> selectedApplications = getSelectedApplications();
        CourseModule courseModule = new CourseModule();

        try
        {
            if (!Int64.TryParse(studentId, out convertedStudentId))
                throw new Exception("System error: Cannot find student ID, please contact administrator.");

            IList<Enrollment> newEnrollment = courseModule.enrollCourse(convertedStudentId, selectedApplications);

            Messenger.setMessage(enroll_course_message, "Courses enrolled successfully!", LEVEL.SUCCESS);
        }
        catch (Exception ex)
        {
            Messenger.setMessage(enroll_course_message, ex.Message, LEVEL.DANGER);
        }
    }
    protected void upload_profile_pic_Click(object sender, EventArgs e)
    {
        try
        {
            long convertedUserId;
            if (!Int64.TryParse(user_id.Text, out convertedUserId))
                throw new Exception("System error: Cannot find user ID, please contact administrator.");

            if (!ProfilePicUploader.HasFile)
                throw new Exception("Please selecte a file.");

            SaveProfilePicFile(convertedUserId, ProfilePicUploader.PostedFile);

        }
        catch (Exception ex)
        {
            Messenger.setMessage(upload_message, ex.Message, LEVEL.DANGER);
        }
    }

    private void SaveProfilePicFile(long userId, HttpPostedFile file)
    {
        FileModule fileModule = new FileModule();
        string filename = file.FileName;
        string extension = System.IO.Path.GetExtension(file.FileName.ToLower());

        if (extension != ".jpeg" &&
            extension != ".jpg" &&
            extension != ".png")
            throw new Exception("You can only upload image files.");

        Session["profile_pic_file"] = fileModule.saveProfilePicForUserId(userId, file.InputStream, file.FileName);
    }
    protected void load_course_Click(object sender, EventArgs e)
    {
        loadCourses();
    }
}