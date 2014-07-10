using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ManageStudents : BaseMemberPage
{
    UserModule userModule = new UserModule();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["studentid"] = null;
            loadStudentList();
        }
    }

    protected void loadStudentList()
    {
        UserModule userModule = new UserModule();
        student_list.DataSource = userModule.getUsersByRole("STUDENT", 0, 9999); //Get it up first before we optimize this

        student_list.DataBind();
    }

    protected void loadStudent(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);
        Button studentButton = (Button)sender;
        string userid = studentButton.CommandArgument;
        long convertedUserid;

        if (Int64.TryParse(userid, out convertedUserid))
        {
            loadStudent(convertedUserid);
            //switchPartner(partner);
            //company_contacts_updatePanel.Update();
            //project_list_panel.Update();
        }
    }

    protected void loadStudent(long studentId)
    {
        
        Student student = (Student)userModule.getUserByUserId(studentId);

        if (student != null)
        {
            Session["studentid"] = student.USER_ID;

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

            //Session["student"] = student.PROJECTS;
            //project_list.DataSource = Session["projects"];
            //project_list.DataBind();
        }

    }

    protected void switchPartner(string prev_id, string current_id)
    {

    }

    protected void UpdateStudentContacts(object sender, EventArgs e)
    {
        //Do validations first
        if (Session["studentid"] == null)
        {
            //EntireManagePartnerPage.Update(); do nothing
            return;
        }

        long studentid;
        if (!Int64.TryParse(Session["studentid"].ToString(), out studentid))
        {
            return; //do nothing
        }

        Student student = (Student)userModule.getUserByUserId(studentid);

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

        try
        {
            userModule.updateUser(student);
            error_message.Controls.Add(new LiteralControl(
                    "<div class='alert alert-success col-sm-10 col-sm-offset-1'>"
                        + "Student updated successfully!"
                        + "</div>"));

            error_modal_control.Show();
            okButton.Text = "Ok";
        }
        catch (Exception ex)
        {
            
            error_message.Controls.Add(new LiteralControl(
                    "<div class='alert alert-danger col-sm-10 col-sm-offset-1'>"
                        + ex.Message
                        + "</div>"));
            
            error_modal_control.Show();
            okButton.Text = "Ok";
            //cancelButton.Visible = false;
        }
        
    }

}