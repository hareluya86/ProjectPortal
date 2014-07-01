using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

    protected void loadPartner(object sender, EventArgs e)
    {
        Button partnerButton = (Button)sender;
        string userid = partnerButton.CommandArgument;
        long convertedUserid;

        if (Int64.TryParse(userid, out convertedUserid))
        {
            UserModule userModule = new UserModule();
            Partner partner = (Partner)userModule.getUserByUserId(convertedUserid);

            if (partner != null)
            {
                loadPartner(partner);
                switchPartner(partner);
            }
        }
    }

    protected void loadPartner(Partner partner)
    {
        company_name.Text = partner.USERNAME;
    }

    protected void switchPartner(Partner partner)
    {
        IEnumerable<Control> result = FindRecursive(Page, c => (c is WebControl) && ((WebControl)c).CssClass.Contains("company-button"));
        if (result != null)
        {
            foreach(Control c in result){
                
            }
        }
    }

    private IEnumerable<Control> FindRecursive(Control c, Func<Control, bool> predicate)
    {
        if (predicate(c))
            yield return c;

        foreach (var child in c.Controls)
        {
            if (predicate(c))
                yield return c;
        }

        foreach (var child in c.Controls)
            foreach (var match in FindRecursive(c, predicate))
                yield return match;
    }
}