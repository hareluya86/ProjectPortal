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
    private static int MAX_FILE_SIZE = 5242880; //5MB

	public FileModule()
	{
        hibernate = new Hibernate();
	}

    public void saveFileForUserId(long userId, HttpPostedFile uploadedFile, string file_type)
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
        if(uploadedFile.ContentLength > MAX_COMMIT_SIZE)
            throw new SaveFileException("File size exceeds "+MAX_FILE_SIZE+" bytes.");
        
        //build save file path by userId
        string saveFilePath = Path.GetDirectoryName(Application.ExecutablePath) 
                                    + IMAGE_DIR + "/" 
                                    + user.USER_ID + "/"
                                    + file_type + "/";

        if(!Directory.Exists(saveFilePath))
            Directory.CreateDirectory(saveFilePath);



    }

    public void checkFile()
}