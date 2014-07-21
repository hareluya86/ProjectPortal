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
    public static int MAX_FLUSH_SIZE = 50;
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

    public IList<Project> getProjectsByOwnerId(long ownerId)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        IList<Project> projects = session.CreateCriteria<Project>()
            .CreateAlias("PROJECT_OWNER","PROJECT_OWNER")
            .Add(Restrictions.Eq("PROJECT_OWNER.USER_ID", ownerId))
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

    /**
     * Returns all project applications made by a given studentId.
     * 
     */
    public IList<ProjectApplication> getProjectApplicationsByStudentId(long studentId)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        IList<ProjectApplication> projectApplications = session.CreateCriteria<ProjectApplication>()
                                                .CreateAlias("APPLICANT", "APPLICANT")
                                                .Add(Restrictions.Eq("APPLICANT.USER_ID", studentId))
                                                .List<ProjectApplication>();

        return projectApplications;
    }

    /**
     * Returns a list of all projects approved by UC starting from index start and maximum limit number of 
     * results.
     * 
     */
    public IList<Project> getAllOpenedProjects(int start, int limit)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        string[] statuses = { APPLICATION_STATUS.APPROVED, APPLICATION_STATUS.ASSIGNED };
        IList<Project> projects = session.CreateCriteria<Project>()
            .Add(Restrictions.In("PROJECT_STATUS", statuses))
            .SetFirstResult(start)
            .SetMaxResults(limit)
            .List<Project>();

        return projects;
    }

    /**
     * Returns a list of all PENDING projects starting from index start and maximum limit number of 
     * results.
     */
    public IList<Project> getAllPendingProjects(int start, int limit)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        IList<Project> projects = session.CreateCriteria<Project>()
            .Add(Restrictions.Eq("PROJECT_STATUS", APPLICATION_STATUS.PENDING))
            .SetFirstResult(start)
            .SetMaxResults(limit)
            .List<Project>();

        return projects;
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
            project.PROJECT_STATUS = APPLICATION_STATUS.APPROVED;
            project.RECOMMENDED_SIZE = 5;
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

    /**
     * 
     */
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

    /**
     * Creates Category objects
     */
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

    /**
     * Creates Category objects
     */
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
     * - Only creates a new project, existing projects will get thrown out as exceptions.
     * - Automatically sets Project status to PENDING.
     * - Checks if Project Title already exists.
     * - Checks if all Contact info is available.
     * - Checks if Contact Email is valid.
     * - Updates the Project Owner of the project with the input ownderId
     * 
     */
    public Project submitProject(Project project, long ownerId)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        this.validateProject(project);

        //Get owner
        Partner owner;
        owner = session.CreateCriteria<Partner>()
                            .Add(Restrictions.Eq("USER_ID", ownerId))
                            .UniqueResult<Partner>();
        if (owner == null || owner.USER_ID == null || owner.USER_ID == 0)
            throw new ProjectSubmissionException("User ID " + ownerId + " does not already exists.");
        
        project.PROJECT_STATUS = APPLICATION_STATUS.PENDING; //set to pending
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

    /**
     * Approve a particular project with a project object
     * 
     * - change the status to APPROVED
     * - validate project atributes
     * - update the project object
     * - send out email to project owner
     * 
     */
    public Project approveProject(Project project)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        if (project.PROJECT_ID == null || project.PROJECT_ID == 0)
            throw new ApproveProjectException("Project " + project.PROJECT_TITLE + " is not submitted yet. Please submit project first.");

        //Set to APPROVED
        project.PROJECT_STATUS = APPLICATION_STATUS.APPROVED;

        //Update project
        Project approvedProject = this.updateProject(project);

        //send out email

        return approvedProject;
    }

    /**
     * Approve a particular project with just project ID and UC comments
     * 
     * - update the project object
     * - change the status to APPROVED
     * - send out email to project owner
     * 
     */
    public Project approveProject(long projectId, string UCComments)
    {
        Project project = this.getProjectById(projectId);
        project.UC_REMARKS = UCComments;
        return this.approveProject(project);
    }

    /**
     * Rejects a project
     * 
     */
    public Project rejectProject(Project project)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        if (project.PROJECT_ID == null || project.PROJECT_ID == 0)
            throw new ApproveProjectException("Project " + project.PROJECT_TITLE + " is not submitted yet. Please submit project first.");

        //Set to APPROVED
        project.PROJECT_STATUS = APPLICATION_STATUS.REJECTED;

        //Update project
        Project approvedProject = this.updateProject(project);

        //send out email

        return approvedProject;
    }

    /**
     * Validates essential project information
     * 
     * - Validate length of project title
     * - Validate if project title exists already
     * - Validate if project contact details are present (mandatory)
     * - Validate if email address is valid
     * 
     */
    public void validateProject(Project project)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        } 
        string title = project.PROJECT_TITLE;

        //Check if project title is less than required
        if (title.Length < MIN_PROJECT_TITLE_LENGTH)
            throw new ProjectValidationException("Project title must be at least " + MIN_PROJECT_TITLE_LENGTH + " char(s).");

        //Check if there is already an existing project with the same title, if the project is not an existing project

        IList<Project> existingProjects = session.CreateCriteria<Project>()
                                        .Add(Restrictions.Or(
                                            Restrictions.Eq("PROJECT_ID", project.PROJECT_ID),
                                            Restrictions.Eq("PROJECT_TITLE", title)))
                                        .List<Project>();
        foreach (Project existingProject in existingProjects)
        {
            if (project.PROJECT_ID != existingProject.PROJECT_ID) //if titles are the same but IDs different, then throw exception
            {
                throw new ProjectValidationException("Project title \"" + title + "\" already exists. Please choose a different title.");
            }
            else
            {
                //Do not validate whether a project is submitted or not because this method can be used in both submit and modify project
                //throw new ProjectValidationException("Project \"" + title + "\" (ID:" + project.PROJECT_ID + ") is already submitted. Please contact administrator to update it.");
            }
        }

        //Check if project has a contact name, number and email
        string contact_name = project.CONTACT_NAME;
        string contact_number = project.CONTACT_NUMBER;
        string contact_email = project.CONTACT_EMAIL;

        if (contact_name.Length <= 0)
            throw new ProjectValidationException("Project contact name cannot be empty.");
        if (contact_number.Length <= 0)
            throw new ProjectValidationException("Project contact number cannot be empty.");
        if (contact_email.Length <= 0)
            throw new ProjectValidationException("Project contact email cannot be empty.");

        //Validate email
        UserModule.validateEmail(contact_email);

        //Validate sizing
        if (project.RECOMMENDED_SIZE <= 0)
            throw new ProjectValidationException("Recommended Size cannot be less than 1.");
        if (project.ALLOCATED_SIZE <= 0)
            throw new ProjectValidationException("Allocated Size cannot be less than 1.");
    }

    /**
     * Update project
     * 
     * - Calls validation method first before updating to database
     * 
     */
    public Project updateProject(Project project)
    {
        //Validate project first
        this.validateProject(project);

        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        session.BeginTransaction();
        session.Save(project);
        session.Transaction.Commit();

        return project;
    }

    /**
     * Assign project to a team of Students
     * 
     * - If rejectOthers is TRUE, set all other Application statuses to REJECTED
     * - Change the application status of all studentIds to APPROVED
     * - Create a Team object with TeamAssignment to the studentIds
     * - For each student who is assigned to this project, close off 
     */
    public Team assignProject(long projectId, IList<long> applicationIds, bool rejectOthers)
    {
        Project project = this.getProjectById(projectId);
        if (project == null)
            throw new ProjectAssignmentException("Project Id " + projectId + " not found.");

        if(project.PROJECT_STATUS != APPLICATION_STATUS.APPROVED)
            throw new ProjectAssignmentException("Project Id " + projectId + " is not in the APPROVED status.");

        if(applicationIds.Count <= 0 )
            throw new ProjectAssignmentException("Please select at least 1 application.");

        IList<ProjectApplication> allApplications = project.APPLICATIONS;
        IList<ProjectApplication> successfulApplications = new List<ProjectApplication>();
        IList<ProjectApplication> failedApplications = new List<ProjectApplication>();

        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        //Update successful applications
        session.BeginTransaction();
        foreach (ProjectApplication application in allApplications)
        {
            if (applicationIds.Contains(application.APPLICATION_ID))
            {
                application.APPLICATION_STATUS = APPLICATION_STATUS.APPROVED;
                successfulApplications.Add(application);
                session.Update(application);
            }

            if (successfulApplications.Count % MAX_FLUSH_SIZE == 0)
                session.Flush();
        }
        session.Flush();

        //If rejectOthers flag is set as true, reject the rest of the applications
        if (rejectOthers)
        {
            foreach (ProjectApplication application in allApplications)
            {
                if (!applicationIds.Contains(application.APPLICATION_ID))
                {
                    application.APPLICATION_STATUS = APPLICATION_STATUS.REJECTED;
                    failedApplications.Add(application);
                    session.Update(application);
                }

                if (failedApplications.Count % MAX_FLUSH_SIZE == 0)
                    session.Flush();
            }
            session.Flush();
        }

        //Set Project status to ASSIGNED
        project.PROJECT_STATUS = APPLICATION_STATUS.ASSIGNED;

        //Create Team first
        Team newTeam = new Team();
        session.Save(newTeam);

        //Create ProjectAssignment - relationship between Team and Project
        ProjectAssignment pAssignment = new ProjectAssignment();
        pAssignment.PROJECT = project;
        pAssignment.TEAM = newTeam;
        project.ASSIGNED_TEAMS.Add(pAssignment);
        newTeam.ASSIGNED_TO_PROJECT.Add(pAssignment);
        
        foreach (ProjectApplication application in successfulApplications)
        {
            //set team relationships
            TeamAssignment tAssignment = new TeamAssignment();
            Student student = application.APPLICANT;
            
            tAssignment.STUDENT = student;
            tAssignment.TEAM = newTeam;

            //Close off all this student's applications
            IList<ProjectApplication> existingApplications = student.PROJECTS_APPLIED;
            foreach(ProjectApplication existingApplication in existingApplications)
            {
                if (existingApplication.APPLICATION_ID == application.APPLICATION_ID)
                    continue;
                existingApplication.APPLICATION_STATUS = APPLICATION_STATUS.REJECTED;
                session.Save(existingApplication);
            }

            //set opposite direction relationships
            newTeam.TEAM_ASSIGNMENT.Add(tAssignment);
            student.TEAM_ASSIGNMENT.Add(tAssignment);

            //set assignment attributes
            tAssignment.ROLE = TEAM_ASSIGNMENT_ROLE.MEMBER;

            session.Save(tAssignment);
            session.Save(student);
        }

        session.Save(pAssignment);
        session.Save(project);
        session.Save(newTeam);
        //session.Flush();

        session.Transaction.Commit();

        return newTeam;
    }

    /**
     * Get the project members for an assigned project
     * 
     * - Returns empty list if project has not been assigned.
     * 
     */
    public IList<Student> getProjectMembers(long projectId)
    {
        IList<Student> members = new List<Student>();

        return members;
    }

}