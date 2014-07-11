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
        if (IsPostBack)
        {

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

    }
}