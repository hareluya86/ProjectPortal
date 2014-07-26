using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ManageUC : BaseMemberPage
{
    UserModule userModule = new UserModule();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["ucid"] = null;
            Session["courses"] = null;
            loadUCList();
        }
    }

    protected void loadUCList()
    {
        UserModule userModule = new UserModule();
        uc_list.DataSource = userModule.getUsersByRole("UNITCOORDINATOR", 0, 9999); //Get it up first before we optimize this

        uc_list.DataBind();
    }

    protected void loadUC(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);
        Button ucButton = (Button)sender;
        string userid = ucButton.CommandArgument;
        long convertedUserid;

        if (Int64.TryParse(userid, out convertedUserid))
        {
            loadUC(convertedUserid);
            //switchPartner(partner);
            //company_contacts_updatePanel.Update();
            uc_contacts_updatePanel.Update();
            course_list_panel.Update();
        }
    }

    protected void loadUC(long UCId)
    {
        CourseModule courseModule = new CourseModule();

        UnitCoordinator uc = (UnitCoordinator)userModule.getUserByUserId(UCId);

        if (uc != null)
        {
            Session["ucid"] = uc.USER_ID;

            first_name.Text = uc.FIRSTNAME;
            last_name.Text = uc.LASTNAME;
            email.Text = uc.EMAIL;
            phone.Text = uc.PHONE;
            address1.Text = uc.ADDRESS1;
            address2.Text = uc.ADDRESS2;
            city_town.Text = uc.CITY_TOWN;
            state.Text = uc.STATE;
            zipcode.Text = uc.ZIP_CODE;
            country.Text = uc.COUNTRY;

            //populate course under UC
            Session["courses"] = courseModule.getCoursesUnderUC(uc.USER_ID);
            course_list.DataSource = Session["courses"];
            course_list.DataBind();

            
            //load profile pic
            FileModule fileModule = new FileModule();
            string profile_pic_address = fileModule.getProfilePicLocation(uc.USER_ID);
            if (profile_pic_address.Length > 0)
            {
                profile_pic.ImageUrl = "~/" + fileModule.getProfilePicLocation(uc.USER_ID);
            }
            else
            {
                profile_pic.ImageUrl = "";
            }
        }

    }

    protected void switchPartner(string prev_id, string current_id)
    {

    }

    protected void UpdateUCContacts(object sender, EventArgs e)
    {
        //Do validations first
        if (Session["ucid"] == null)
        {
            //EntireManagePartnerPage.Update(); do nothing
            return;
        }

        long convertedUCId;
        if (!Int64.TryParse(Session["ucid"].ToString(), out convertedUCId))
        {
            return; //do nothing
        }

        UnitCoordinator uc = (UnitCoordinator)userModule.getUserByUserId(convertedUCId);

        uc.FIRSTNAME = first_name.Text;
        uc.LASTNAME = last_name.Text;
        uc.EMAIL = email.Text;
        uc.PHONE = phone.Text;
        uc.ADDRESS1 = address1.Text;
        uc.ADDRESS2 = address2.Text;
        uc.CITY_TOWN = city_town.Text;
        uc.STATE = state.Text;
        uc.ZIP_CODE = zipcode.Text;
        uc.COUNTRY = country.Text;

        try
        {
            userModule.updateUser(uc);
            Messenger.setMessage(error_message, "UC updated successfully!", LEVEL.SUCCESS);
            error_modal_control.Show();
            okButton.Text = "Ok";
        }
        catch (Exception ex)
        {
            Messenger.setMessage(error_message, ex.Message, LEVEL.DANGER);
            error_modal_control.Show();
            okButton.Text = "Ok";
        }
        
    }

}