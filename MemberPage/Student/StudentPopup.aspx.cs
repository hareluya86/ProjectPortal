using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentPopup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            long convertedStudentId;
            string studentId = Request.QueryString["studentid"];
            if(Int64.TryParse(studentId, out convertedStudentId))
            {
                loadStudent(convertedStudentId);
            }
            else
            {
                throw new Exception("Cannot find student ID, please contact administrator.");
            }
        }
    }

    private void loadStudent(long studentId)
    {
        UserModule userModule = new UserModule();
        Student student = (Student) userModule.getUserByUserId(studentId);

        student_id.Text = student.USER_ID.ToString();
        first_name.Text = student.FIRSTNAME;
        last_name.Text = student.LASTNAME;
        email.Text = student.EMAIL;
        student_writeup.Text = student.WRITE_UP;
    }
}