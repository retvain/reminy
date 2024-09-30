namespace Reminy.Core.Domain.Entity;

public sealed class User
{
    public long Id { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Password { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime LastModifiedDate { get; private set; }

    public User(
        long id,
        string email,
        string firstName,
        string lastName,
        string password,
        DateTime createdDate,
        DateTime lastModifiedDate)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        CreatedDate = createdDate;
        LastModifiedDate = lastModifiedDate;
    }

    public void SetId(long id)
    {
        Id = id;
    }

    public void SetEmail(string email)
    {
        Email = email;
    }

    public void SetFirstName(string firstName)
    {
        FirstName = firstName;
    }

    public void SetLastName(string lastName)
    {
        LastName = lastName;
    }

    public void SetPassword(string password)
    {
        Password = password;
    }

    public void SetLastModifiedDate(DateTime lastModifiedDate)
    {
        LastModifiedDate = lastModifiedDate;
    }
}