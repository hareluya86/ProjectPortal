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


}