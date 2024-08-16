using Microsoft.EntityFrameworkCore;
using StarWall.Domain.BlogEntities;
using StarWall.Domain.ContactEntities;
using StarWall.Domain.UserEntities;
using StarWall.Domain.WallpaperEntities;
using StarWall.Domain.WebsiteInfoEntities;
using System.Reflection;

namespace StarWall.Persistence.DataBaseContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    #region DataBase Tables

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<WebSiteInfo> WebSiteInfos { get; set; }
    public DbSet<Wallpaper> Wallpapers { get; set; }
    public DbSet<WallpaperImage> WallpaperImages { get; set; }
    public DbSet<WallpaperDeviceCategory> WallpaperDeviceCategories { get; set; }
    public DbSet<WallpaperGenreCategory> WallpaperGenreCategories { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Like> Likes { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}