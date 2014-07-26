using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UploadedFile
/// </summary>
public class UploadedFile
{
    public virtual Int64 UPLOADEDFILE_ID { get; set; }
    public virtual string UPLOADEDFILE_NAME { get; set; }
    public virtual string UPLOADEDFILE_TYPE { get; set; }
    public virtual string UPLOADEDFILE_PATH { get; set; }

    public virtual UserAccount UPLOADEDFILE_OWNER { get; set; }

	public UploadedFile()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}