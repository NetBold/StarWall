using StarWall.Domain.UserEntities;

namespace StarWall.Domain.WallpaperEntities;

public class Wallpaper
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public long DownloadsCount { get; set; }
    public long ViewsCount { get; set; }
    public string Source { get; set; }
    public string Description { get; set; }

    public long UploaderId { get; set; }
    public User? Uploader { get; set; }
    
    public long WallpaperGenreCategoryId { get; set; }
    public WallpaperGenreCategory? WallpaperGenreCategory { get; set; }

    public virtual List<Like>? Likes { get; set; }

    public virtual List<WallpaperImage>? WallpaperImages { get; set; }

    public DateTime PublishDate { get; set; }
}