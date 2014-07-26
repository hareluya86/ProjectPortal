using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CourseModule
/// </summary>
public class CourseModule
{
    private Hibernate hibernate;
    private ISession session;
    public static int MAX_FLUSH_SIZE = 50;
    public static int MAX_COMMIT_SIZE = 100;

    //Business-related numbers
    

    public CourseModule()
	{
        hibernate = new Hibernate();
	}

    /**
     * Gets a list of courses from start to limit
     * 
     */
    public IList<Course> getCourses(int start, int limit)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        IList<Course> courses = session.CreateCriteria<Course>()
            .SetFirstResult(start)
            .SetMaxResults(limit)
            .List<Course>();

        return courses;
    }

    /**
     * Creates Course objects
     */
    public IList<Course> createCourses(IList<string> courseNames, long UCId)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        //check if category already exist
        IList<Course> existingCourses = session.CreateCriteria<Course>()
            .Add(Restrictions.In("COURSE_NAME", courseNames.ToArray()))
            .List<Course>();

        if (existingCourses != null && existingCourses.Count > 0)
        {
            string errorMessage = "Courses ";
            foreach (Course course in existingCourses)
            {
                errorMessage += "\"" + course.COURSE_NAME + "\", ";
            }
            errorMessage += " already exists!";
            throw new CourseException(errorMessage);
        }

        IList<Course> courses = new List<Course>();

        session.BeginTransaction();
        foreach (string courseName in courseNames)
        {
            Course course = new Course();
            course.COURSE_NAME = courseName;
            course.COURSE_COORDINATOR_ID = UCId;
            session.Save(course);
            courses.Add(course);
        }
        session.Transaction.Commit();

        return courses;
    }

    /**
     * Creates Course objects
     */
    public IList<Course> createCourses(string commaDelimitedCourses, long UCId)
    {
        IList<string> categories = commaDelimitedCourses.Split(new char[] { '\n', ',' });
        IList<string> trimmedAndNiceCourses = new List<string>();
        //Trim
        for (int i = 0; i < categories.Count; i++)
        {
            String course = categories.ElementAt(i);
            trimmedAndNiceCourses.Insert(i, course.Trim());

        }
        return createCourses(trimmedAndNiceCourses, UCId);
    }

    /**
     * Enroll course
     * 
     * 
     * 
     */
    public IList<Enrollment> enrollCourse(long studentId, IList<long> courseIds)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        //get student and check his/her enrollment first for existing enrollment
        Student student = session.CreateCriteria<Student>()
                            .Add(Restrictions.Eq("USER_ID", studentId))
                            .UniqueResult<Student>();

        if (student == null)
            throw new CourseEnrollmentException("Invalid student ID: " + studentId);

        if(courseIds.Count <= 0)
            throw new CourseEnrollmentException("No course selected.");

        IList<Enrollment> enrolledCourses = student.COURSE_ENROLLED;
        foreach (Enrollment enrolledCourse in enrolledCourses)
        {
            if (courseIds.Contains(enrolledCourse.COURSE.COURSE_ID))
                throw new CourseEnrollmentException("Student " + studentId + " is already enrolled in course " + enrolledCourse.COURSE.COURSE_NAME);
        }

        //get course objects
        long[] courseIdsArray = courseIds.ToArray();
        IList<Course> courses = session.CreateCriteria<Course>()
                                    .Add(Restrictions.In("COURSE_ID",courseIdsArray))
                                    .List<Course>();

        IList<Enrollment> newEnrollments = new List<Enrollment>();
        session.BeginTransaction();
        foreach (Course course in courses)
        {
            Enrollment newEnrollment = new Enrollment();
            newEnrollment.COURSE = course;
            newEnrollment.STUDENT = student;
            course.ENROLLMENTS.Add(newEnrollment);
            student.COURSE_ENROLLED.Add(newEnrollment);

            session.Save(newEnrollment);
            session.Save(course);
            session.Save(student);

            newEnrollments.Add(newEnrollment);
        }
        session.Transaction.Commit();

        return newEnrollments;
    }

    public IList<Course> getCoursesUnderUC(long UCId)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        IList<Course> courses = session.CreateCriteria<Course>()
                                    .Add(Restrictions.Eq("COURSE_COORDINATOR_ID", UCId))
                                    .List<Course>();

        return courses;
    }
}