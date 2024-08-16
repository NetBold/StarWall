using StarWall.Domain.UserEntities;

namespace StarWall.Domain.WallpaperEntities;

public class Like
{
    public long Id { get; set; }
    
    public virtual User? Liker { get; set; }
    public long? LikerId { get; set; }

    public virtual Wallpaper? Wallpaper { get; set; }
    public long? WallpaperId { get; set; }
}