using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Mapping;
using NHibernate.Tool.hbm2ddl;
using System.Web;
using System.IO;

/// <summary>
/// Helper class for database layer
/// </summary>
public class Hibernate
{
    private ISession session;
    private Configuration cfg;

    public void init()
    {
        cfg = new Configuration();
        cfg.Configure();

        /* Add xml mappings created manually
         * - Because Web Site projects are only compiled during runtime, the hibernate mapping files will need to 
         * be discovered and added to the assembly at runtime.
         */
        cfg.AddDirectory(new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Mapping")));

        //Add your custom configurations here to override the Hibernate.xml configuration.
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
}