using SQLite;

public enum UserRole { User, Business }

[Table("Users")]
public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Unique]
    public string Email { get; set; }

    public string HashedPassword { get; set; }
    public string Salt { get; set; }
    public UserRole Role { get; set; }
    public string FullName { get; set; }
}
