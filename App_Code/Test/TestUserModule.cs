using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TestUserModule
/// </summary>
public class TestUserModule
{
    Hibernate hibernate;
    private ISession session;
    public static int MAX_COMMIT_SIZE = 100;

	public TestUserModule()
	{
        hibernate = new Hibernate();
	}

    public IList<UserAccount> insertUserAccount(int numUsers)
    {
        UserModule userModule = new UserModule();
        return userModule.insertUserAccount(numUsers);
    }

    public string encodePassword(string rawPassword)
    {
        UserModule userModule = new UserModule();
        return userModule.encodePassword(rawPassword);
    }

    public bool checkEncryptedPassword(string rawPassword, string hash)
    {
        UserModule userModule = new UserModule();
        return userModule.checkEncryptedPassword(rawPassword, hash);
    }

    public void login(string userid, string password)
    {
        UserModule userModule = new UserModule();
        userModule.login(userid, password);
    }
}