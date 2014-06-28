using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Mapping;
using NHibernate.Tool.hbm2ddl;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;

/// <summary>
/// Summary description for TestHibernateClass
/// </summary>
public class TestHibernateClass
{
    private ISession session;
    private Configuration cfg;
    public static int MAX_COMMIT_SIZE = 100;

    public void init()
    {
        cfg = new Configuration();
        cfg.Configure();

        /* Add xml mappings created manually
         * - Because Web Site projects are only compiled during runtime, the hibernate mapping files will need to 
         * be discovered and added to the assembly at runtime.
         */
        //cfg.AddDirectory(new DirectoryInfo(HttpContext.Current.Server.MapPath("~/App_Code")));
        cfg.AddDirectory(new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Mapping")));

        //Add all classes
        //cfg.AddAssembly(typeof(UserAccount).Assembly);

    }

    public ISession getSession()
    {
        if (cfg == null)
        {
            init();
        }
        ISessionFactory SessionFactory = cfg.BuildSessionFactory();
        return SessionFactory.OpenSession();
    }

    public void generate_schema()
    {
        if (cfg == null)
        {
            init();
        }

        new SchemaExport(cfg).Execute(false, true, false);

    }

    public IList<UserAccount> insertUserAccount(int numUsers)
    {
        IList<UserAccount> results = new List<UserAccount>();
        if (session == null || !session.IsOpen)
        {
            session = this.getSession();
        }
        
        session.BeginTransaction();
        for (int i = 0; i < numUsers; i++)
        {
            Student user = new Student();

            //set all test variable here
            user.PASSWORD = "Password " + i;
            user.FIRSTNAME = "First name " + i;

            //save
            session.Save(user);
            
            //add to list
            results.Add(user);


            if ((i+1) % MAX_COMMIT_SIZE == 0)
            {
                session.Flush();
                session.Transaction.Commit();
                session.BeginTransaction();
            }
        }

        session.Transaction.Commit();

        return results;
    }
}