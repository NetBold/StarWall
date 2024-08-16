namespace StarWall.Domain.WallpaperEntities;

public class WallpaperDeviceCategory
{
    public int Id { get; set; }
    public string? Title { get; set; }
    
    public List<WallpaperImage>? WallpaperImages { get; set; }
}