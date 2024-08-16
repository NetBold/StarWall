using StarWall.Core.Interfaces;
using StarWall.Domain.BlogEntities;
using StarWall.Domain.ContactEntities;
using StarWall.Domain.UserEntities;
using StarWall.Domain.WallpaperEntities;
using StarWall.Domain.WebsiteInfoEntities;
using StarWall.Persistence.DataBaseContext;

namespace StarWall.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    private IGenericRepository<Wallpaper> _wallpapers;
    private IGenericRepository<WallpaperDeviceCategory> _wallpaperDeviceCategories;
    private IGenericRepository<WallpaperGenreCategory> _wallpaperGenreCategories;
    private IGenericRepository<User> _users;
    private IGenericRepository<Role> _roles;
    private IGenericRepository<Blog> _blogs;
    private IGenericRepository<Contact> _contacts;
    private IGenericRepository<WebSiteInfo> _webSiteInfos;
    private IGenericRepository<Like> _likes;
    public IGenericRepository<WallpaperImage> _wallpaperImages;

    public IGenericRepository<Wallpaper> Wallpapers => _wallpapers ??= new GenericRepository<Wallpaper>(_context);
    public IGenericRepository<WallpaperDeviceCategory> WallpaperDeviceCategories => _wallpaperDeviceCategories ??= new GenericRepository<WallpaperDeviceCategory>(_context);
    public IGenericRepository<WallpaperGenreCategory> WallpaperGenreCategories => _wallpaperGenreCategories ??= new GenericRepository<WallpaperGenreCategory>(_context);
    public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(_context);
    public IGenericRepository<Role> Roles => _roles ??= new GenericRepository<Role>(_context);
    public IGenericRepository<Blog> Blogs => _blogs ??= new GenericRepository<Blog>(_context);
    public IGenericRepository<Contact> Contacts => _contacts ??= new GenericRepository<Contact>(_context);
    public IGenericRepository<WebSiteInfo> WebSiteInfos => _webSiteInfos ??= new GenericRepository<WebSiteInfo>(_context);
    public IGenericRepository<Like> Likes => _likes ??= new GenericRepository<Like>(_context);
    public IGenericRepository<WallpaperImage> WallpaperImages => _wallpaperImages ??= new GenericRepository<WallpaperImage>(_context);

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}