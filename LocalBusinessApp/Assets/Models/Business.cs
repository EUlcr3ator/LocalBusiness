using SQLite;

[Table("Businesses")]
public class Business
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int OwnerId { get; set; } // Foreign key to User.Id
    public string Name { get; set; }
    public string Description { get; set; }
    public string ContactInfo { get; set; }
    public string WorkingHours { get; set; }
    public string Category { get; set; }
    public string ImagePath { get; set; }
}