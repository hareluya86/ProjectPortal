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

    private void updatePassword(long partnerId, string old_password, string new_password)
    {
        UserModule userModule = new UserModule();
        userModule.setPassword(partnerId, old_password, new_password);
    }

    protected void updateDetails(object sender, EventArgs e)
    {
        long partnerid;
        System.Threading.Thread.Sleep(3000);

        try
        {
            if (Int64.TryParse(Session["userid"].ToString(), out partnerid))
                updateDetails(partnerid);//from login
            else
                throw new Exception("Cannot find user ID, please contact administrator.");

            if (password.Text != null && password.Text.Length > 0)
            {
                password_popup.Show();
                hiddenPassword.Value = password.Text;
            }
            else
            {
                error_modal_control.Show();
                Messenger.setMessage(error_message, "Details updated successfully!", LEVEL.SUCCESS);
            }
        }
        catch (InvalidEmailAddressException ieaex)
        {
            Messenger.setMessage(error_message, ieaex.Message, LEVEL.DANGER);
            error_modal_control.Show();
        }
        catch (Exception ex)
        {
            Messenger.setMessage(error_message, ex.Message, LEVEL.DANGER);
            error_modal_control.Show();
        }
        finally
        {
            
        }
        
    }

    protected void ConfirmPasswordChange(object sender, EventArgs e)
    {
        long partnerid;
        try
        {
            if (!Int64.TryParse(Session["userid"].ToString(), out partnerid))
                throw new Exception("Cannot find user ID, please contact administrator.");

            if (!hiddenPassword.Value.Equals(new_password.Text))
                throw new Exception("Passwords don't match.");

            updatePassword(partnerid,old_password.Text,new_password.Text);//from login
            error_modal_control.Show();
            Messenger.setMessage(error_message, "Password updated successfully!", LEVEL.SUCCESS);
            NewProjectUpdatePanel.Update();
        }
        catch (LoginException lex)
        {
            Messenger.setMessage(ChangePasswordMessage, lex.Message, LEVEL.DANGER);
            password_popup.Show();
        }
        catch (Exception ex)
        {
            Messenger.setMessage(ChangePasswordMessage, ex.Message, LEVEL.DANGER);
            password_popup.Show();
        }
    }

    /**
     * Clears only required fields like passwords
     */
    private void clearFields()
    {
        password.Text = "";
        old_password.Text = "";
        new_password.Text = "";
        hiddenPassword.Value = "";
    }
    protected void ClearFields(object sender, EventArgs e)
    {
        clearFields();
    }
}