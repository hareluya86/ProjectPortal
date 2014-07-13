using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

/// <summary>
/// Summary description for Messenger
/// </summary>
public class Messenger
{
    

    public static void setMessage(PlaceHolder placeholder, string message, string level)
    {
        placeholder.Controls.Add(new LiteralControl(
                    "<div class='alert alert-"+level+" col-sm-10 col-sm-offset-1'>"
                        + message
                        + "</div>"));
    }


}