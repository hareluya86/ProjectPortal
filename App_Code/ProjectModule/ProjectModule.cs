using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectModule
/// </summary>
public class ProjectModule
{
    private Hibernate hibernate;
    private ISession session;
    public static int MAX_COMMIT_SIZE = 100;

	public ProjectModule()
	{
        hibernate = new Hibernate();
	}

    public IList<Project> getProjectsByIds(List<Int64> projectIds)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        var projects = session.CreateCriteria<Project>()
            .Add(Restrictions.In("PROJECT_ID", projectIds))
            .List<Project>();

        return projects;
    }

    public void deleteProjects(IList<Int64> projectIds)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        } 
        IList<Project> projects = new List<Project>();
        foreach (Int64 projectId in projectIds)
        {
            projects.Add(session.Load<Project>(projectId));
        }
        this.deleteProjects(projects);
    }

    public void deleteProjects(IList<Project> projects)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        session.BeginTransaction();
        for (int i = 0; i < projects.Count; i++ )
        {
            Project project = projects.ElementAt(i);
            session.Delete(project);

            if ((i + 1) % MAX_COMMIT_SIZE == 0)
            {
                session.Flush();
                session.Transaction.Commit();
                session.BeginTransaction();
            }
        }
        session.Transaction.Commit();
    }

    //For testing only!
    public IList<Project> createRandomProjects(long partnerId, int numProjects)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        Partner partner = (Partner)session.Get("Partner", partnerId);
        IList<Project> results = partner.PROJECTS;

        session.BeginTransaction();
        for (int i = 0; i < numProjects; i++)
        {
            Project project = new Project();
            project.PROJECT_TITLE = "Project " + i + " by partner " + partnerId;
            project.PROJECT_OWNER = partner;
            results.Add(project);
            
            session.Save(partner);
            session.Save(project);

            if ((i + 1) % MAX_COMMIT_SIZE == 0)
            {
                session.Flush();
                session.Transaction.Commit();
                session.BeginTransaction();
            }
        }

        session.Transaction.Commit();

        return results;
    }

    public ProjectApplication ApplyProject(long studentId, long projectId)
    {
        return null;
    }
}