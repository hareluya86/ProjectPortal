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

    //Business-related numbers
    private const int MIN_PROJECT_TITLE_LENGTH = 5;

	public ProjectModule()
	{
        hibernate = new Hibernate();
	}

    public Project getProjectById(long projectId)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        var project = session.CreateCriteria<Project>()
            .Add(Restrictions.Eq("PROJECT_ID", projectId))
            .UniqueResult<Project>();

        return project;
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
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        Student student = session.CreateCriteria<Student>()
            .Add(Restrictions.Eq("USER_ID", studentId))
            .UniqueResult<Student>();

        //Check if both entities exists
        if (student == null)
            throw new ProjectApplicationException("Student ID " + studentId + " cannot be found.");

        Project project = this.getProjectById(projectId);

        if (project == null)
            throw new ProjectApplicationException("Project ID " + projectId + " cannot be found.");

        //Check student already has an application to the project
        IList<ProjectApplication> existingAppls = student.PROJECTS_APPLIED;
        foreach(ProjectApplication existingAppl in existingAppls){
            if (existingAppl.PROJECT.PROJECT_ID == projectId)
            {
                throw new ProjectApplicationException("Student ID " + studentId
                    + " already has application to project "
                    + existingAppl.PROJECT.PROJECT_TITLE + " (" + existingAppl.PROJECT.PROJECT_ID + ").");
            }
        }

        ProjectApplication appl = new ProjectApplication();
        appl.APPLICANT = student;
        appl.PROJECT = project;
        appl.APPLICATION_STATUS = APPLICATION_STATUS.PENDING;
        student.PROJECTS_APPLIED.Add(appl);
        project.APPLICATIONS.Add(appl);

        session.BeginTransaction();
        session.Save(appl);
        session.Transaction.Commit();

        return appl;
    }

    public Category createCategory(string categoryName)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        //check if category already exist
        IList<Category> existingCateogies = session.CreateCriteria<Category>()
            .Add(Restrictions.Eq("CATEGORY_NAME", categoryName))
            .List<Category>();

        if (existingCateogies != null && existingCateogies.Count > 0)
            throw new CategoryException("Category " + categoryName + " already exists!");

        Category category = new Category();
        category.CATEGORY_NAME = categoryName;

        session.BeginTransaction();
        session.Save(category);
        session.Transaction.Commit();

        return category;

    }

    public IList<Category> createCategories(IList<string> categoryNames)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        //check if category already exist
        IList<Category> existingCategories = session.CreateCriteria<Category>()
            .Add(Restrictions.In("CATEGORY_NAME", categoryNames.ToArray()))
            .List<Category>();

        if (existingCategories != null && existingCategories.Count > 0)
        {
            string errorMessage = "Categories ";
            foreach (Category cat in existingCategories)
            {
                errorMessage += "\"" + cat.CATEGORY_NAME + "\", ";
            }
            errorMessage += " already exists!";
            throw new CategoryException(errorMessage);
        }

        IList<Category> categories = new List<Category>();

        session.BeginTransaction();
        foreach (string categoryName in categoryNames)
        {
            Category category = new Category();
            category.CATEGORY_NAME = categoryName;
            session.Save(category);
            categories.Add(category);
        }
        session.Transaction.Commit();

        return categories;
    }

    public IList<Category> createCategories(string commaDelimitedCategories)
    {
        IList<string> categories = commaDelimitedCategories.Split(new char[] { '\n', ',' });
        IList<string> trimmedAndNiceCategories = new List<string>();
        //Trim
        for (int i = 0; i < categories.Count; i++)
        {
            String cat = categories.ElementAt(i);
            trimmedAndNiceCategories.Insert(i, cat.Trim());
            
        }
        return createCategories(trimmedAndNiceCategories);
    }

    public IList<Category> getAllCategories(int start, int limit)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        IList<Category> allCategories = session.CreateCriteria<Category>()
                                            .SetFirstResult(start)
                                            .SetMaxResults(limit)
                                            .List<Category>();
        return allCategories;
    }

    /**
     * Submits a new project.
     * 
     * - If project already exists in the database, updates it. A project exist only if a provided PROJECT_ID can be 
     * found in the database.
     */
    public Project submitProject(Project project, long ownerId)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        //Do all validations here

        string title = project.PROJECT_TITLE;
        Project foundExistingProject = null;
        //if (project.PROJECT_ID != null)
        //{
        //    foundExistingProject = this.getProjectById(project.PROJECT_ID);
        //}

        //Check if project title is less than required
        if(title.Length < MIN_PROJECT_TITLE_LENGTH)
            throw new ProjectSubmissionException("Project title must be at least " + MIN_PROJECT_TITLE_LENGTH+" char(s).");

        //Check if there is already an existing project with the same title, if the project is not an existing project
        if (foundExistingProject == null)
        {
            IList<Project> existingProjects = session.CreateCriteria<Project>()
                                            .Add(Restrictions.Eq("PROJECT_TITLE", title))
                                            .List<Project>();
            foreach (Project existingProject in existingProjects)
            {
                if (project.PROJECT_ID != existingProject.PROJECT_ID) //if titles are the same but IDs different, then throw exception
                {
                //don't check title first for debugging other issues    throw new ProjectSubmissionException("Project title \"" + title + "\" already exists. Please choose a different title.");
                }
            }
        }

        //Check if project has a contact name, number and email
        string contact_name = project.CONTACT_NAME;
        string contact_number = project.CONTACT_NUMBER;
        string contact_email = project.CONTACT_EMAIL;

        if (contact_name.Length <= 0)
            throw new ProjectSubmissionException("Project contact name cannot be empty.");
        if (contact_number.Length <= 0)
            throw new ProjectSubmissionException("Project contact number cannot be empty.");
        if (contact_email.Length <= 0)
            throw new ProjectSubmissionException("Project contact email cannot be empty.");

        //Validate email
        UserModule.validateEmail(contact_email);

        //Get owner
        Partner owner;
        if (foundExistingProject == null)
        {
            owner = session.CreateCriteria<Partner>()
                                .Add(Restrictions.Eq("USER_ID", ownerId))
                                .UniqueResult<Partner>();
            if (owner == null || owner.USER_ID == null)
                throw new ProjectSubmissionException("User ID " + ownerId + " does not already exists.");
        }
        else
        {
            owner = foundExistingProject.PROJECT_OWNER;
        }
        
        project.PROJECT_OWNER = owner;
        owner.PROJECTS.Add(project);

        //ready to persist to database
        session.BeginTransaction();
        session.Save(project);
        session.Transaction.Commit();

        return project;

    }

    /**
     * Register an existing project with the provided category IDs
     * 
     * Project must be persisted in the database at this point. If the project is already registered with 
     * a provided category, this method will skip that category.
     * 
     */
    public void registerProjectCategories(Project project, IList<Int64> categoryIds)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        if(project.PROJECT_ID == null || project.PROJECT_ID == 0)
            throw new ProjectCategoryRegistrationException("Project "+project.PROJECT_TITLE+" is not submitted yet. Please submit project first.");

        Project existingProject = session.CreateCriteria<Project>()
                            .Add(Restrictions.Eq("PROJECT_ID", project.PROJECT_ID))
                            .UniqueResult<Project>();
        if(existingProject == null)
            throw new ProjectCategoryRegistrationException("Project " + project.PROJECT_TITLE + " is not submitted yet. Please submit project first.");

        //Validate all selected categories exist
        IList<Category> existingCategories = session.CreateCriteria<Category>()
                                                .Add(Restrictions.In("CATEGORY_ID", categoryIds.ToArray()))
                                                .List<Category>();

        if (existingCategories.Count != categoryIds.Count) //we can only do this if categoryId is unique
            throw new ProjectCategoryRegistrationException("Some categories do not exist and must be created first.");

        //start creating ProjectCategory objects to link the 2 up
        //impt! use those objects retrieved from the database in the above steps
        session.BeginTransaction();
        foreach (Category category in existingCategories)
        {
            ProjectCategory projectCategory = new ProjectCategory();
            projectCategory.CATEGORY = category;
            projectCategory.PROJECT = existingProject;
            existingProject.CATEGORIES.Add(projectCategory);
            category.CATEGORIES.Add(projectCategory);

            session.Save(projectCategory);

        }
        session.Save(existingProject);
        
        session.Transaction.Commit();

    }
}