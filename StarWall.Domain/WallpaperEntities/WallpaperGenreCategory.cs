namespace StarWall.Domain.WallpaperEntities;

public class WallpaperGenreCategory
{
    public long Id { get; set; }
    public string? Title { get; set; }

    public List<Wallpaper>? Wallpapers { get; set; }
}