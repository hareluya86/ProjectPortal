using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //Show "Please log in" message if it is a redirect from any of the member pages
            if (Session["previous_url"] != null)
            {
                string previous_url = Session["previous_url"].ToString();
                login_message.Controls.Add(new LiteralControl(
                    "<div class='alert alert-danger col-sm-10 col-sm-offset-1'>"
                        + "Your session has ended, please log in again."
                        + "</div>"));
            }
        }
        
    }

    protected void Login(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);
        UserModule userModule = new UserModule();
        string userid = input_userid.Text;
        string password = input_password.Text;

        try
        {
            UserAccount authenticatedUser = userModule.login(userid, password);

            SessionIDManager sessionIdManager = new SessionIDManager();
            string newId = sessionIdManager.CreateSessionID(Context);

            

            Session["Userid"] = userid;
            Session["Username"] = authenticatedUser.USERNAME;
            Session["UserRole"] = authenticatedUser.ROLE;
            //Session["Sessionid"] = Session.SessionID; //Unnecessary

            if (Session["previous_url"] != null)
            {
                string previous_url = Session["previous_url"].ToString();
                Session["previous_url"] = ""; //Clear session variable just in case
                Response.Redirect(previous_url);
            }

            string redirectURL = UserRoleDispatcher.getPageByUserRole(authenticatedUser.ROLE);
            if (redirectURL.Length <= 0)
            {
                throw new Exception("No role configured for " + authenticatedUser.ROLE + " yet, please contact administrator.");
            }

            Response.Redirect(redirectURL);

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