using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdateDetails : BaseMemberPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            long partnerid;
            if(Int64.TryParse(Session["userid"].ToString(), out partnerid))
                loadDetails(partnerid);//from login
        }

    }

    private void loadDetails(long partnerId)
    {
        UserModule userModule = new UserModule();
        Partner partner = (Partner)userModule.getUserByUserId(partnerId);

        company_name.Text = partner.USERNAME;
        email.Text = partner.EMAIL;
        phone.Text = partner.PHONE;
        fax.Text = partner.FAX;
        address1.Text = partner.ADDRESS1;
        address2.Text = partner.ADDRESS2;
        city_town.Text = partner.CITY_TOWN;
        state.Text = partner.STATE;
        zipcode.Text = partner.ZIP_CODE;
        country.Text = partner.COUNTRY;

    }

    private void updateDetails(long partnerId)
    {
        UserModule userModule = new UserModule();
        Partner partner = (Partner)userModule.getUserByUserId(partnerId);

        partner.USERNAME = company_name.Text;
        partner.EMAIL = email.Text;
        partner.PHONE = phone.Text;
        partner.FAX = fax.Text;
        partner.ADDRESS1 = address1.Text;
        partner.ADDRESS2 = address2.Text;
        partner.CITY_TOWN = city_town.Text;
        partner.STATE = state.Text;
        partner.ZIP_CODE = zipcode.Text;
        partner.COUNTRY = country.Text;

        userModule.updateUser(partner);
    } 

    protected void updateDetails(object sender, EventArgs e)
    {
        long partnerid;
        if (Int64.TryParse(Session["userid"].ToString(), out partnerid))
            updateDetails(partnerid);//from login
    }
}