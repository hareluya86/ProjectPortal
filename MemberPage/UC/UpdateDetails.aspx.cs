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
            long convertedUCId;
            if (Int64.TryParse(Session["userid"].ToString(), out convertedUCId))
                loadDetails(convertedUCId);//from login
        }

    }

    private void loadDetails(long UCId)
    {
        UserModule userModule = new UserModule();
        UnitCoordinator uc = (UnitCoordinator)userModule.getUserByUserId(UCId);

        //student.Text = partner.USERNAME;
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

    }

    private void updateDetails(long UCId)
    {
        UserModule userModule = new UserModule();
        UnitCoordinator uc = (UnitCoordinator)userModule.getUserByUserId(UCId);

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

        userModule.updateUser(uc);
        
    }

    private void updatePassword(long UCId, string old_password, string new_password)
    {
        UserModule userModule = new UserModule();
        userModule.setPassword(UCId, old_password, new_password);
    }

    protected void updateDetails(object sender, EventArgs e)
    {
        long UCId;
        System.Threading.Thread.Sleep(3000);

        try
        {
            if (Int64.TryParse(Session["userid"].ToString(), out UCId))
                updateDetails(UCId);//from login
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
        long convertedUCId;
        try
        {
            if (!Int64.TryParse(Session["userid"].ToString(), out convertedUCId))
                throw new Exception("Cannot find user ID, please contact administrator.");

            if (!hiddenPassword.Value.Equals(new_password.Text))
                throw new Exception("Passwords don't match.");

            updatePassword(convertedUCId, old_password.Text, new_password.Text);//from login
            error_modal_control.Show();
            Messenger.setMessage(error_message, "Password updated successfully!", LEVEL.SUCCESS);
            UpdateUCDetailPanel.Update();
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

    protected void ResetButton_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}