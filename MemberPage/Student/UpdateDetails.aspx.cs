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
            long convertedStudentId;
            if (Int64.TryParse(Session["userid"].ToString(), out convertedStudentId))
                loadDetails(convertedStudentId);//from login
        }

    }

    private void loadDetails(long studentId)
    {
        UserModule userModule = new UserModule();
        Student student = (Student)userModule.getUserByUserId(studentId);

        //student.Text = partner.USERNAME;
        student_id.Text = student.USER_ID.ToString();
        first_name.Text = student.FIRSTNAME;
        last_name.Text = student.LASTNAME;
        email.Text = student.EMAIL;
        phone.Text = student.PHONE;
        address1.Text = student.ADDRESS1;
        address2.Text = student.ADDRESS2;
        city_town.Text = student.CITY_TOWN;
        state.Text = student.STATE;
        zipcode.Text = student.ZIP_CODE;
        country.Text = student.COUNTRY;
        writeup.Text = student.WRITE_UP;

    }

    private void updateDetails(long studentId)
    {
        UserModule userModule = new UserModule();
        Student student = (Student)userModule.getUserByUserId(studentId);

        student.FIRSTNAME = first_name.Text;
        student.LASTNAME = last_name.Text;
        student.EMAIL = email.Text;
        student.PHONE = phone.Text;

        student.ADDRESS1 = address1.Text;
        student.ADDRESS2 = address2.Text;
        student.CITY_TOWN = city_town.Text;
        student.STATE = state.Text;
        student.ZIP_CODE = zipcode.Text;
        student.COUNTRY = country.Text;

        student.WRITE_UP = writeup.Text;

        userModule.updateUser(student);
        
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
            UpdateStudentDetailPanel.Update();
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