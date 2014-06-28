using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administrator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            AdminMultiView.ActiveViewIndex = 0; //show login box
        }
        else
        {
            AdminMultiView.ActiveViewIndex = 1; //show control panel
        }
    }

    protected void Login(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(5000);
        UserModule userModule = new UserModule();
        string userid = input_userid.Text;
        string password = input_password.Text;

        try
        {
            UserAccount authenticatedUser = userModule.login(userid, password);
            //Check user role
            //Not yet implemented

            Session["User"] = userid;
            /*login_message.Controls.Add(new LiteralControl(
                "<div class='alert alert-success col-sm-10 col-sm-offset-1'>"
                    + "Login successful!"
                    + "</div>"));
             */

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

}