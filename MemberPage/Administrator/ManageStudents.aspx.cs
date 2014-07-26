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
            project_application_list_panel.Update();
            assigned_project_panel.Update();
            course_list_panel.Update();
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

            //populate projects applied for
            Session["applied_projects"] = student.PROJECTS_APPLIED;
            project_application_list.DataSource = Session["applied_projects"];
            project_application_list.DataBind();

            //populate assigned project
            if (student.TEAM_ASSIGNMENT.Count >= 1)
            {
                TeamAssignment tAssignment = student.TEAM_ASSIGNMENT.First(); //assume that there will only be 1 assignment per student
                Team assignedTeam = tAssignment.TEAM;
                ProjectAssignment pAssignment = assignedTeam.ASSIGNED_TO_PROJECT.First();
                Project assignedProject = pAssignment.PROJECT;

                project_title.Text = assignedProject.PROJECT_TITLE;
                project_company.Text = assignedProject.PROJECT_OWNER.USERNAME;
                contact_person.Text = assignedProject.CONTACT_NAME;
                contact_number.Text = assignedProject.CONTACT_NUMBER;
            }
            
            //load profile pic
            FileModule fileModule = new FileModule();
            string profile_pic_address = fileModule.getProfilePicLocation(student.USER_ID);
            if (profile_pic_address.Length > 0)
            {
                profile_pic.ImageUrl = "~/" + fileModule.getProfilePicLocation(student.USER_ID);
            }
            else
            {
                profile_pic.ImageUrl = "";
            }

            //load course enrolled
            course_list.DataSource = student.COURSE_ENROLLED;
            course_list.DataBind();
        }

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