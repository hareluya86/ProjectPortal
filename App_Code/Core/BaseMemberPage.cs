﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Core
/// </summary>
public class BaseMemberPage : System.Web.UI.Page
{
    public BaseMemberPage()
    {
        this.Load += new EventHandler(this.Page_Load);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //redirect to the login page if user is not logged in
        if (Session["Userid"] == null || //Scenario 1 - User navigates to any member pages from non-member pages
            Session.SessionID == null || //Scenario 2 - User navigates to any member pages from external places
            !Session.SessionID.Equals(Request.Cookies["ASP.NET_SessionId"].Value.ToString())) //Scenario 3 - User's session timeout
        {
            Session["previous_url"] = Request.Url.AbsoluteUri;
            Response.Redirect("/LoginPage.aspx");
        }
    }
}