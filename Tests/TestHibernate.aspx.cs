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
        TestHibernateClass test = new TestHibernateClass();
        Session["userList"] = test.insertUserAccount(Convert.ToInt32(TestInsertUserAccount_NumberOfUsers_Textbox.Text));

        InsertedUserTable.DataSource = Session["userList"];
        InsertedUserTable.DataBind();
    }

    public void InsertedUserTable_PageIndexChanging(object sender, DataGridPageChangedEventArgs  e)
    {
        if (sender != null)
        {
            InsertedUserTable.CurrentPageIndex = e.NewPageIndex;
            InsertedUserTable.DataSource = Session["userList"];
            InsertedUserTable.DataBind();
        }
    }
}