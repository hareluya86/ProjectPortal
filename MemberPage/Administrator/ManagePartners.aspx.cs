using HtmlAgilityPack;
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
        System.Threading.Thread.Sleep(3000);
        Button partnerButton = (Button)sender;
        string userid = partnerButton.CommandArgument;
        long convertedUserid;

        if (Int64.TryParse(userid, out convertedUserid))
        {
            loadPartner(convertedUserid);
            //switchPartner(partner);
            company_contacts_updatePanel.Update();
            project_list_panel.Update();
        }
    }

    protected void loadPartner(long partnerId)
    {
        UserModule userModule = new UserModule();
        Partner partner = (Partner)userModule.getUserByUserId(partnerId);

        if (partner != null)
        {
            Session["partnerid"] = partner.USER_ID;

            company_name.Text = partner.USERNAME;
            company_reg_no.Text = partner.COMPANY_REG_NUM;
            email.Text = partner.EMAIL;
            phone.Text = partner.PHONE;
            fax.Text = partner.FAX;
            address1.Text = partner.ADDRESS1;
            address2.Text = partner.ADDRESS2;
            city_town.Text = partner.CITY_TOWN;
            state.Text = partner.STATE;
            zipcode.Text = partner.ZIP_CODE;
            country.Text = partner.COUNTRY;

            Session["projects"] = partner.PROJECTS;
            project_list.DataSource = Session["projects"];
            project_list.DataBind();
        }

    }

    protected void switchPartner(string prev_id, string current_id)
    {

    }

    protected void Delete_Projects(object sender, EventArgs e)
    {
        IList<Int64> projects = new List<Int64>();
        /*try
        {*/
        string collectionProjectIds = Request.Form["selected"];
        string[] projectIdsStrings = collectionProjectIds.Split(',');
        foreach (string projectIdString in projectIdsStrings)
        {
            long longValue = Convert.ToInt64(projectIdString);
            projects.Add(longValue);
        }

        ProjectModule projectModule = new ProjectModule();
        projectModule.deleteProjects(projects);
        error_modal_control.Show();
        delete_projects_error_message.Text = "Success!";

        long partnerid;
        if (Session["partnerid"] != null &&
            Int64.TryParse(Session["partnerid"].ToString(),out partnerid))
        {
            this.loadPartner(partnerid);
            project_list_panel.Update();
        }
        
        /*}
        catch (Exception ex)
        {
            error_modal_control.Show();
            delete_projects_error_message.Text = ex.Message;
        }*/

    }
}