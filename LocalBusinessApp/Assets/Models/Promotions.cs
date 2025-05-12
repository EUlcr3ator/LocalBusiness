using SQLite;
using System;

[Table("Promotions")]
public class Promotions
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int BusinessId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}