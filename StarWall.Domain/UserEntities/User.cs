using StarWall.Domain.BlogEntities;
using StarWall.Domain.WallpaperEntities;

namespace StarWall.Domain.UserEntities;

public class User
{
    public long Id { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? HashedPassword { get; set; }
    public string? ProfileImage { get; set; }
    public DateTime RegisterDate { get; set; }

    public string? ActiveToken { get; set; }

    public bool IsActive { get; set; }

    public int RoleId { get; set; }
    public Role? Role { get; set; }

    public List<Wallpaper>? UploadedWallpapers { get; set; }

    public List<Wallpaper>? DownloadedWallpapers { get; set; }

    public List<Like>? Likes { get; set; }

    public List<Blog>? Blogs { get; set; }
}