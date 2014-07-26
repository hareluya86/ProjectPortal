using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//this is renamed such that the other pages don't accidentally reference to this when they use Partner
public partial class StudentPage : BaseMemberPage 
{
    public StudentPage()
        : base()
    {
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //redirect to default page
        Response.Redirect("~/MemberPage/Student/ProjectStatus.aspx");
    }

}