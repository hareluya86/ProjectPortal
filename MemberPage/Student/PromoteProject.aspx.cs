using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PromoteProject : BaseMemberPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
        else
        {
        }

    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        if (FileUploader.HasFile)
        {

        }
        else
        {
            Messenger.setMessage(upload_message, "Please provide a file.", LEVEL.DANGER);
        }

        UploadFilePanel.Update();
    }

    protected void SaveFile(HttpPostedFile file)
    {
        
    }
}