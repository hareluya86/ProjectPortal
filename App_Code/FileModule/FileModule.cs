using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;

/// <summary>
/// Summary description for FileModule
/// </summary>
public class FileModule
{
    private Hibernate hibernate;
    private ISession session;
    public static int MAX_COMMIT_SIZE = 100;

    private static string IMAGE_DIR = "/Images/";
    private static int MAX_FILE_SIZE = 52428800; //50MB

    private static string[] ALLOWED_PROFILEPIC_TYPES = { "jpg", "jpeg", "png"};

	public FileModule()
	{
        hibernate = new Hibernate();
	}

    /**
     * Saves file for a particular user
     * 
     * - Saves in the directory format: /Files/[userid]/[file_type]/[file_name]
     *
     */
    public UploadedFile saveFileForUserId(long userId, Stream inputStream, string file_type, string file_name)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        //validate userID first by retrieving the UserAccount object
        UserAccount user = session.CreateCriteria<UserAccount>()
                            .Add(Restrictions.Eq("USER_ID",userId))
                            .UniqueResult<UserAccount>();

        if(user == null)
            throw new SaveFileException("User "+userId+" does not exist.");

        //validate file size
        if (inputStream.Length > MAX_FILE_SIZE)
            throw new SaveFileException("File size exceeds "+MAX_FILE_SIZE+" bytes.");
        
        //build save file path by userId
        string relativeFilePath = "Files"+ "\\"
                                    + user.USER_ID + "\\"
                                    + file_type + "\\";
        string saveFilePath = HttpRuntime.AppDomainAppPath + relativeFilePath;

        if(!Directory.Exists(saveFilePath))
            Directory.CreateDirectory(saveFilePath);

        saveFilePath += file_name;
        relativeFilePath += file_name;

        //create file stream
        var saveFileStream = File.Create(saveFilePath);
        inputStream.Seek(0,SeekOrigin.Begin);
        inputStream.CopyTo(saveFileStream);
        saveFileStream.Close();

        //create UploadedFile object
        UploadedFile uploadedFile = new UploadedFile();
        uploadedFile.UPLOADEDFILE_NAME = file_name;
        uploadedFile.UPLOADEDFILE_PATH = relativeFilePath;
        uploadedFile.UPLOADEDFILE_TYPE = file_type;
        uploadedFile.UPLOADEDFILE_OWNER = user;
        user.FILES.Add(uploadedFile);

        session.BeginTransaction();
        session.Save(uploadedFile);
        session.Save(user);
        session.Transaction.Commit();

        return uploadedFile;
    }

    /**
     * Saves profile pic for a particular user
     * 
     * - Saves in the directory format: /Files/[userid]/[FILE_TYPE.PROFILE_PIC name]/profile_pic.[ALLOWED_PROFILEPIC_TYPES]
     * 
     */
    public UploadedFile saveProfilePicForUserId(long userId, Stream inputStream, string file_name)
    {
        string extension = System.IO.Path.GetExtension(file_name.ToLower());

        if(ALLOWED_PROFILEPIC_TYPES.Contains(extension))
            throw new SaveFileException("User "+userId+" does not exist.");

        string newFileName = FILE_TYPE.PROFILE_PIC+extension;

        return saveFileForUserId(userId, inputStream, FILE_TYPE.PROFILE_PIC, newFileName);
    }

    /**
     * Get profile pic location for a given userId
     * 
     * 
     */
    public string getProfilePicLocation(long userId)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        string result = "";
        UploadedFile profilePic = session.CreateCriteria<UploadedFile>()
                                    .CreateAlias("UPLOADEDFILE_OWNER", "UPLOADEDFILE_OWNER")
                                    .Add(Restrictions.Eq("UPLOADEDFILE_OWNER.USER_ID", userId))
                                    .Add(Restrictions.Eq("UPLOADEDFILE_TYPE", FILE_TYPE.PROFILE_PIC))
                                    .UniqueResult<UploadedFile>();
        if (profilePic != null)
            result = profilePic.UPLOADEDFILE_PATH;
        return result;
    }

    /**
     * Save file for project
     * 
     * - For projects, no project needs to be created yet to upload a file, so a ProjectDocument is first created
     * without tagging to a project. 
     * 
     */
    public ProjectDocument saveProjectFile(Stream inputStream, string file_type, string file_name)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        //validate file size
        if (inputStream.Length > MAX_FILE_SIZE)
            throw new SaveFileException("File size exceeds " + MAX_FILE_SIZE + " bytes.");

        //build save file path by userId
        string relativeFilePath = "Files" + "\\"
                                    + "Project" + "\\"
                                    + file_type + "\\";
        string saveFilePath = HttpRuntime.AppDomainAppPath + relativeFilePath;

        if (!Directory.Exists(saveFilePath))
            Directory.CreateDirectory(saveFilePath);

        saveFilePath += file_name;
        relativeFilePath += file_name;

        //create file stream
        var saveFileStream = File.Create(saveFilePath);
        inputStream.Seek(0, SeekOrigin.Begin);
        inputStream.CopyTo(saveFileStream);
        saveFileStream.Close();

        //create UploadedFile object
        ProjectDocument projectDocument = new ProjectDocument();
        projectDocument.PROJECTFILE_NAME = file_name;
        projectDocument.PROJECTFILE_PATH = relativeFilePath;
        projectDocument.PROJECTFILE_TYPE = file_type;

        session.BeginTransaction();
        session.Save(projectDocument);
        session.Transaction.Commit();

        return projectDocument;
    }

    /**
     * Update project document owner (Project)
     * 
     * 
     */
    public ProjectDocument updateProjectDocumentOwner(long projectDocumentId, long projectId)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        ProjectDocument projectDocument = session.CreateCriteria<ProjectDocument>()
                                            .Add(Restrictions.Eq("PROJECTFILE_ID", projectDocumentId))
                                            .UniqueResult<ProjectDocument>();

        projectDocument.PROJECTFILE_OWNER = projectId; //no validations!
        session.BeginTransaction();
        session.Update(projectDocument);
        session.Transaction.Commit();

        return projectDocument;
    }

    /**
     * Get project documents by project ID
     * 
     * - only return the first one
     * 
     */
    public ProjectDocument getProjectDocumentByProjectId(long projectId)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        IList<ProjectDocument> projectDocuments = session.CreateCriteria<ProjectDocument>()
                                    .Add(Restrictions.Eq("PROJECTFILE_OWNER", projectId))
                                    .List<ProjectDocument>();
        if (projectDocuments.Count > 0)
            return projectDocuments.First();
        return null;
    }
}