namespace StarWall.Domain.ContactEntities;

public class Contact
{
    public long Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Body { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsSeenByAdmin { get; set; }
}