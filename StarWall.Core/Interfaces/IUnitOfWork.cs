using StarWall.Domain.BlogEntities;
using StarWall.Domain.ContactEntities;
using StarWall.Domain.UserEntities;
using StarWall.Domain.WallpaperEntities;
using StarWall.Domain.WebsiteInfoEntities;

namespace StarWall.Core.Interfaces;

public interface IUnitOfWork
{
    public IGenericRepository<Wallpaper> Wallpapers { get; }
    public IGenericRepository<WallpaperDeviceCategory> WallpaperDeviceCategories { get; }
    public IGenericRepository<WallpaperGenreCategory> WallpaperGenreCategories { get; }
    public IGenericRepository<User> Users { get; }
    public IGenericRepository<Role> Roles { get; }
    public IGenericRepository<Blog> Blogs { get; }
    public IGenericRepository<Contact> Contacts { get; }
    public IGenericRepository<WebSiteInfo> WebSiteInfos { get; }
    public IGenericRepository<Like> Likes { get; }
    public IGenericRepository<WallpaperImage> WallpaperImages { get; }
    Task Save();
}