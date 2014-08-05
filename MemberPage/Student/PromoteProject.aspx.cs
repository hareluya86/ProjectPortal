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

    protected void UploadFileButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (!FileUploader.HasFile)
                throw new Exception("Please provide a file.");

            long convertedStudentId;
            if(!Int64.TryParse(Session["userid"].ToString(), out convertedStudentId))
                throw new Exception("Cannot find user ID, please contact administrator.");

            string filename = FileUploader.FileName;
            SaveZipFile(convertedStudentId,FileUploader.PostedFile);
            //Messenger.setMessage(upload_file_message, "File uploaded successfully.", LEVEL.SUCCESS);
        }
        catch (SaveFileException sfex)
        {
            Messenger.setMessage(upload_file_message, sfex.Message, LEVEL.DANGER);
        }
        catch(Exception ex)
        {
            Messenger.setMessage(upload_file_message, ex.Message, LEVEL.DANGER);
        }
        finally
        {
            refreshUploadedFiles();
            UploadFilePanel.Update();
        }
    }

    protected void SaveZipFile(long userId, HttpPostedFile file)
    {
        FileModule fileModule = new FileModule();
        string filename = file.FileName;
        string extension = System.IO.Path.GetExtension(file.FileName.ToLower()); 

        if (extension != ".zip")
            throw new Exception("You can only upload zip files.");

        hidden_uploaded_file_ID.Value = fileModule.saveFileForUserId(userId, file.InputStream, FILE_TYPE.ZIP, file.FileName).UPLOADEDFILE_ID.ToString();
        hidden_uploaded_file_name.Value = filename;
    }

    protected void SaveVideoFile(long userId, HttpPostedFile file)
    {
        FileModule fileModule = new FileModule();
        string filename = file.FileName;
        string extension = System.IO.Path.GetExtension(file.FileName.ToLower());

        if (extension != ".mp4")
            throw new Exception("You can only upload mp4 files.");

        hidden_uploaded_video_ID.Value = fileModule.saveFileForUserId(userId, file.InputStream, FILE_TYPE.VIDEO, file.FileName).UPLOADEDFILE_ID.ToString();
        hidden_uploaded_video_name.Value = filename;
    }

    protected void UploadVideoButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (!VideoUploader.HasFile)
                throw new Exception("Please provide a file.");

            long convertedStudentId;
            if (!Int64.TryParse(Session["userid"].ToString(), out convertedStudentId))
                throw new Exception("Cannot find user ID, please contact administrator.");

            string filename = FileUploader.FileName;
            SaveVideoFile(convertedStudentId, VideoUploader.PostedFile);
            
            //Messenger.setMessage(upload_video_message, "File uploaded successfully.", LEVEL.SUCCESS);
            
        }
        catch (SaveFileException sfex)
        {
            Messenger.setMessage(upload_video_message, sfex.Message, LEVEL.DANGER);
        }
        catch (Exception ex)
        {
            Messenger.setMessage(upload_video_message, ex.Message, LEVEL.DANGER);
        }
        finally
        {
            refreshUploadedFiles();
            UploadFilePanel.Update();
        }

    }
    protected void submit_project_button_Click(object sender, EventArgs e)
    {
        try
        {
            ProjectPromotion projectPromotion = new ProjectPromotion();

            if (project_title.Text.Length <= 0)
                throw new Exception("Please enter project title.");
            projectPromotion.PROMOTION_TITLE = project_title.Text;

            if (project_writeup.Text.Length <= 0)
                throw new Exception("Please write something about the project in Project Write Up.");
            projectPromotion.PROMOTION_WRITEUP = project_writeup.Text;

            if (website.Text.Length <= 0)
                throw new Exception("Please provide a project website.");
            projectPromotion.PROMOTION_WEBSITE = website.Text;

            if (Session["zip_file"] == null)
                throw new Exception("Please upload a zip file.");
            string zipFile = Session["zip_file"].ToString();
            long convertedZipFileId;
            Int64.TryParse(zipFile, out convertedZipFileId);
            projectPromotion.PROMOTION_ZIPFILE_ID = convertedZipFileId;

            if (Session["video_file"] == null)
                throw new Exception("Please upload a video file.");
            string videoFile = Session["video_file"].ToString();
            long convertedVideoFileId;
            Int64.TryParse(zipFile, out convertedVideoFileId);
            projectPromotion.PROMOTION_VIDEOFILE_ID = convertedVideoFileId;

            long convertedStudentId;
            if (!Int64.TryParse(Session["userid"].ToString(), out convertedStudentId))
                throw new Exception("Cannot find user ID, please contact administrator.");

            ProjectModule projectModule = new ProjectModule();
            projectPromotion = projectModule.promoteProject(convertedStudentId, projectPromotion);
            Messenger.setMessage(error_message, "Project is submitted successfully!", LEVEL.SUCCESS);
        }
        catch (Exception ex)
        {
            
            Messenger.setMessage(error_message, ex.Message, LEVEL.DANGER);
        }
        finally
        {
            error_modal_control.Show();
            refreshUploadedFiles();
        }
    }

    private void refreshUploadedFiles()
    {
        if (hidden_uploaded_file_ID.Value != null && hidden_uploaded_file_ID.Value.Length > 0)
        {
            Messenger.setMessage(upload_file_message, "File " + hidden_uploaded_file_name.Value + " uploaded successfully.",LEVEL.SUCCESS);
        }

        if (hidden_uploaded_video_ID.Value != null && hidden_uploaded_video_ID.Value.Length > 0)
        {
            Messenger.setMessage(upload_video_message, "File " + hidden_uploaded_video_name.Value + " uploaded successfully.", LEVEL.SUCCESS);
        }
    }
}