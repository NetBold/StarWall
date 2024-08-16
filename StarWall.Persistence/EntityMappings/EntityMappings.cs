using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWall.Core.Security;
using StarWall.Domain.BlogEntities;
using StarWall.Domain.ContactEntities;
using StarWall.Domain.UserEntities;
using StarWall.Domain.WallpaperEntities;
using StarWall.Domain.WebsiteInfoEntities;

namespace StarWall.Persistence.EntityMappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Username).IsRequired().HasMaxLength(30);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.ProfileImage).IsRequired();
        builder.Property(x => x.HashedPassword).IsRequired();
        builder.Property(x => x.ActiveToken).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.RegisterDate).IsRequired();
        builder.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId);
        builder.HasMany(x => x.UploadedWallpapers).WithOne(x => x.Uploader).HasForeignKey(x => x.UploaderId);
        builder.HasMany(x => x.DownloadedWallpapers);
        builder.HasMany(x => x.Blogs).WithOne(x => x.Writer).HasForeignKey(x => x.WriterId);
        builder.HasData(new User()
        {
            Id = 1,
            RoleId = 1,
            Username = "MainUploader",
            FirstName = "Main",
            LastName = "Main",
            HashedPassword = PasswordHelper.EncodePasswordMd5("Sa8611"),
            RegisterDate = DateTime.Now,
            Email = "zolfisahand1386@gmail.com",
            ProfileImage = "Default.png",
            ActiveToken = "NullToken",
            IsActive = true
        });
        builder.HasData(new User()
        {
            Id = 2,
            RoleId = 3,
            Username = "MainAdmin",
            FirstName = "Main",
            LastName = "Main",
            HashedPassword = PasswordHelper.EncodePasswordMd5("Sa8611"),
            RegisterDate = DateTime.Now,
            Email = "zolfisahand1386@gmail.com",
            ProfileImage = "Default.png",
            ActiveToken = "NullToken",
            IsActive = true
        });
    }
}

public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
        builder.HasData(new Role() { Id = 1, Title = "Uploader" }, new Role() { Id = 2, Title = "NormalUser" }, new Role() { Id = 3, Title = "Admin" });
    }
}

public class ContactMapping : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(110);
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Body).IsRequired();
        builder.Property(x => x.CreationDate).IsRequired();
        builder.Property(x => x.IsSeenByAdmin).IsRequired();
    }
}

public class WebSiteInfoMapping : IEntityTypeConfiguration<WebSiteInfo>
{
    public void Configure(EntityTypeBuilder<WebSiteInfo> builder)
    {
        builder.ToTable("WebSiteInfos");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(70);
        builder.Property(x => x.PhoneNumber1).IsRequired().HasMaxLength(11);
        builder.Property(x => x.PhoneNumber2).IsRequired().HasMaxLength(11);
        builder.Property(x => x.ShortDescription).IsRequired();
        builder.Property(x => x.LongDescription).IsRequired();
        builder.Property(x => x.PoweredBy).IsRequired();
        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Instagram);
        builder.Property(x => x.Telegram);
        builder.Property(x => x.Twitter);
        builder.HasData(new WebSiteInfo() { Id = 1, Address = "Iran - Tabriz - Valiasr Street", Email = "starsite.iran@gmail.com", PhoneNumber1 = "09031813679", PhoneNumber2 = "09031813679", ShortDescription = "Wallpapers like Stars", LongDescription = "Download amazing wallpapers from starwall and set it as your backgroun image on your devices.", Instagram = "StarWall_inst", Telegram = "StarWall_Tel", Twitter = "StarWall_Twi", PoweredBy = "NetBold", Title = "StarWall" });
    }
}

public class WallpaperMapping : IEntityTypeConfiguration<Wallpaper>
{
    public void Configure(EntityTypeBuilder<Wallpaper> builder)
    {
        builder.ToTable("Wallpapers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(120);
        builder.Property(x => x.DownloadsCount).IsRequired();
        builder.Property(x => x.ViewsCount).IsRequired();
        builder.Property(x => x.PublishDate).IsRequired();
        builder.Property(x => x.Source);
        builder.Property(x => x.Description).IsRequired();
        builder.HasOne(x => x.WallpaperGenreCategory).WithMany(x => x.Wallpapers)
            .HasForeignKey(x => x.WallpaperGenreCategoryId);
        builder.HasMany(x => x.Likes).WithOne(x => x.Wallpaper).HasForeignKey(x => x.WallpaperId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class WallpaperImageMapping : IEntityTypeConfiguration<WallpaperImage>
{
    public void Configure(EntityTypeBuilder<WallpaperImage> builder)
    {
        builder.ToTable("WallpaperImages");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FileName).IsRequired();
        builder.HasOne(x => x.WallpaperDeviceCategory).WithMany(x => x.WallpaperImages).HasForeignKey(x => x.WallpaperDeviceCategoryId);
        builder.HasOne(x => x.Wallpaper).WithMany(x => x.WallpaperImages).HasForeignKey(x => x.WallpaperId);
    }
}

public class WallpaperDeviceCategoryMapping : IEntityTypeConfiguration<WallpaperDeviceCategory>
{
    public void Configure(EntityTypeBuilder<WallpaperDeviceCategory> builder)
    {
        builder.ToTable("WallpaperDeviceCategories");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
        builder.HasData(
            new WallpaperDeviceCategory()
            {
                Id = 1,
                Title = "Mobile"
            },
            new WallpaperDeviceCategory()
            {
                Id = 2,
                Title = "Desktop"
            }
        );
    }
}

public class WallpaperGenreCategoryMapping : IEntityTypeConfiguration<WallpaperGenreCategory>
{
    public void Configure(EntityTypeBuilder<WallpaperGenreCategory> builder)
    {
        builder.ToTable("WallpaperGenreCategories");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
        builder.HasData(
            new WallpaperGenreCategory()
            {
                Id = 1,
                Title = "Movie"
            },
            new WallpaperGenreCategory()
            {
                Id = 2,
                Title = "Nature"
            },
            new WallpaperGenreCategory()
            {
                Id = 3,
                Title = "Games"
            },
            new WallpaperGenreCategory()
            {
                Id = 4,
                Title = "Anime"
            },
            new WallpaperGenreCategory()
            {
                Id = 5,
                Title = "Minimalist"
            }
        );
    }
}

public class LikeMapping : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.ToTable("Likes");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Liker).WithMany(x => x.Likes).HasForeignKey(x => x.LikerId).OnDelete(DeleteBehavior.Restrict);
    }
}

public class BlogMapping : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable("Blogs");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(120);
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Body).IsRequired();
        builder.Property(x => x.CreationDateTime).IsRequired();
        builder.Property(x => x.LastEditedDateTime).IsRequired();
        builder.Property(x => x.ViewsCount).IsRequired();
        builder.HasOne(x => x.Editor).WithMany().HasForeignKey(x => x.EditorId);
    }
}