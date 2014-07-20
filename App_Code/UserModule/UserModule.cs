using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserModule
/// </summary>
public class UserModule
{
    private Hibernate hibernate;
    private ISession session;
    public static int MAX_COMMIT_SIZE = 100;
    private const string mySalt = "pRojeCt~p0rtal&439x052349k5k3a9b";
    
    public UserModule()
	{
        hibernate = new Hibernate();
	}

    public UserAccount login(string userid, string password)
    {
        if (userid == null || userid.Length <= 0)
            throw new LoginException("Please enter userid.");
        if (password == null || password.Length <= 0)
            throw new LoginException("Please enter password.");

        long convertedUserid;
        if (!Int64.TryParse(userid, out convertedUserid))
            throw new LoginException("Userid is invalid.");

        //find user
        UserAccount user = this.getUserByUserId(convertedUserid);
        if (user == null)
            throw new LoginException("Invalid credentials");

        //verify password
        bool loginSuccess = this.checkEncryptedPassword(password, user.PASSWORD);
        if (!loginSuccess)
        {
            throw new LoginException("Invalid credentials");
        }

        return user;

    }

    public string encodePassword(string rawPassword)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        string hash = BCrypt.Net.BCrypt.HashPassword(rawPassword+mySalt, salt);
        return hash;
    }

    public bool checkEncryptedPassword(string rawPassword, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(rawPassword+mySalt, hash);
    }

    public UserAccount getUserByUserId(Int64 userid)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        var user = session.CreateCriteria<UserAccount>()
            .Add(Restrictions.Eq("USER_ID", userid))
            .UniqueResult<UserAccount>();
        return user;
    }

    public IList<UserAccount> getUsersByRole(string role, int start, int limit)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        var users = session.CreateCriteria<UserAccount>()
            .Add(Restrictions.Eq("ROLE", role))
            .SetFirstResult(0)
            .SetMaxResults(limit)
            .List<UserAccount>();
        
        return users;
    }

    public void updateUser(UserAccount user)
    {
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }
        //do validations here
        validateEmail(user.EMAIL);

        session.BeginTransaction();
        session.Update(user);
        session.Transaction.Commit();
    }

    public static void validateEmail(string email)
    {
        string[] checkAt = email.Split('@');
        if (checkAt.Length < 2)
            throw new InvalidEmailAddressException("Email address does not contain @");

        if(checkAt.Length > 2)
            throw new InvalidEmailAddressException("Email address contains more than one @");

        if(email.Contains('/') || email.Contains('\\') || email.Contains('*') || email.Contains('\''))
            throw new InvalidEmailAddressException("Email address contains illegal characters.");
    }

    public void setPassword(long userId, string oldPassword, string newPassword)
    {
        UserAccount user = this.login(userId.ToString(), oldPassword);
        if (user == null)
            throw new LoginException("Invalid credentials");

        user.PASSWORD = this.encodePassword(newPassword);
        this.updateUser(user);
    }

    /* Testing method */
    public IList<UserAccount> insertUserAccount(int numUsers)
    {
        
        IList<UserAccount> results = new List<UserAccount>();
        if (session == null || !session.IsOpen)
        {
            session = hibernate.getSession();
        }

        session.BeginTransaction();
        for (int i = 0; i < numUsers; i++)
        {
            UserAccount user;
            if (i % 4 == 1)
            {
                Student student = new Student();
                student.FIRSTNAME = "Student " + i%10;
                student.USERNAME = student.FIRSTNAME;
                student.PASSWORD = this.encodePassword("password"+i);

                user = student;
            }
            else if (i % 4 == 0)
            {
                Admin admin = new Admin();
                admin.PASSWORD = this.encodePassword("password" + i);
                admin.USERNAME = "admin" + i%10;
                user = admin;

            }
            else if (i % 4 == 2)
            {
                Partner partner = new Partner();
                partner.PASSWORD = this.encodePassword("password" + i);
                partner.USERNAME = "company " + i%10;
                user = partner;
            }
            else if(i % 4 == 3)
            {
                UnitCoordinator uc = new UnitCoordinator();
                uc.FIRSTNAME = "Unit Coordinator " + i % 10;
                uc.PASSWORD = this.encodePassword("password" + i);

                user = uc;
            }
            else
            {
                Student student = new Student();
                student.FIRSTNAME = "Student " + i % 10;
                student.USERNAME = student.FIRSTNAME;
                student.PASSWORD = this.encodePassword("password" + i);

                user = student;
            }

            //save
            session.Save(user);

            //add to list
            results.Add(user);


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
}