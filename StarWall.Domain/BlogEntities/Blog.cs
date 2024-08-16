using StarWall.Domain.UserEntities;

namespace StarWall.Domain.BlogEntities;

public class Blog
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Body { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime LastEditedDateTime { get; set; }
    public long ViewsCount { get; set; }

    public long? WriterId { get; set; }
    public virtual User? Writer { get; set; }
    
    public long? EditorId { get; set; }
    public virtual User? Editor { get; set; }
}