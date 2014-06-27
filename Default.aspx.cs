using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            /*SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            string checkUser = "select Role from userInfo where Username = '" +TextBoxUN.Text+ "'";
            SqlCommand com = new SqlCommand(checkUser, conn);
            string Role = com.ExecuteScalar().ToString().Trim();
            if ( Role == "admin") 
            {
                Response.Redirect("admin.aspx");
            }
            conn.Close();*/
        

        }
    }
}