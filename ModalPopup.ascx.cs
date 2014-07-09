using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ModalPopup : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnOk.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnOk.UniqueID, "");
    }

    public void ShowMessage(string Message)
    {
        lblMessage.Text = Message;
        //lblCaption.Text = "";
        //tdCaption.Visible = false;
        mpext.Show();
    }

    public void ShowMessage(string Message, string Caption)
    {
        lblMessage.Text = Message;
        //lblCaption.Text = Caption;
        //tdCaption.Visible = true;
        mpext.Show();
    }

    private void Hide()
    {
        lblMessage.Text = "";
        //lblCaption.Text = "";
        mpext.Hide();
    }

    public void btnOk_Click(object sender, EventArgs e)
    {
        OnOkButtonPressed(e);
    }

    public delegate void OkButtonPressedHandler(object sender, EventArgs args);
    public event OkButtonPressedHandler OkButtonPressed;
    protected virtual void OnOkButtonPressed(EventArgs e)
    {
        if (OkButtonPressed != null)
            OkButtonPressed(btnOk, e);
    }
}