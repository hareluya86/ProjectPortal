using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ManagePartners : BaseMemberPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadPartnerList();
        }
    }

    protected void loadPartnerList()
    {
        UserModule userModule = new UserModule();
        company_list.DataSource = userModule.getUsersByRole("PARTNER", 0, 9999); //Get it up first before we optimize this
        company_list.DataBind();
    }
}