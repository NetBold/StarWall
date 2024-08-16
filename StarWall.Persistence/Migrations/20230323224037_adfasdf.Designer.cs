﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StarWall.Persistence.DataBaseContext;

#nullable disable

namespace StarWall.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230323224037_adfasdf")]
    partial class adfasdf
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StarWall.Domain.BlogEntities.Blog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("EditorId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastEditedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<long>("ViewsCount")
                        .HasColumnType("bigint");

                    b.Property<long?>("WriterId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EditorId");

                    b.HasIndex("WriterId");

                    b.ToTable("Blogs", (string)null);
                });

            modelBuilder.Entity("StarWall.Domain.ContactEntities.Contact", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(110)
                        .HasColumnType("nvarchar(110)");

                    b.Property<bool>("IsSeenByAdmin")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Contacts", (string)null);
                });

            modelBuilder.Entity("StarWall.Domain.UserEntities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Uploader"
                        },
                        new
                        {
                            Id = 2,
                            Title = "NormalUser"
                        },
                        new
                        {
                            Id = 3,
                            Title = "Admin"
                        });
                });

            modelBuilder.Entity("StarWall.Domain.UserEntities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ActiveToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ProfileImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ActiveToken = "NullToken",
                            Email = "zolfisahand1386@gmail.com",
                            FirstName = "Main",
                            HashedPassword = "19-A5-14-18-9B-61-5A-F5-CE-2F-60-C1-F4-97-46-6C",
                            IsActive = true,
                            LastName = "Main",
                            ProfileImage = "Default.png",
                            RegisterDate = new DateTime(2023, 3, 24, 3, 10, 37, 621, DateTimeKind.Local).AddTicks(5436),
                            RoleId = 1,
                            Username = "MainUploader"
                        },
                        new
                        {
                            Id = 2L,
                            ActiveToken = "NullToken",
                            Email = "zolfisahand1386@gmail.com",
                            FirstName = "Main",
                            HashedPassword = "19-A5-14-18-9B-61-5A-F5-CE-2F-60-C1-F4-97-46-6C",
                            IsActive = true,
                            LastName = "Main",
                            ProfileImage = "Default.png",
                            RegisterDate = new DateTime(2023, 3, 24, 3, 10, 37, 621, DateTimeKind.Local).AddTicks(5493),
                            RoleId = 3,
                            Username = "MainAdmin"
                        });
                });

            modelBuilder.Entity("StarWall.Domain.WallpaperEntities.Like", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("LikerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("WallpaperId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LikerId");

                    b.HasIndex("WallpaperId");

                    b.ToTable("Likes", (string)null);
                });

            modelBuilder.Entity("StarWall.Domain.WallpaperEntities.Wallpaper", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DownloadsCount")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<long>("UploaderId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("ViewsCount")
                        .HasColumnType("bigint");

                    b.Property<long>("WallpaperGenreCategoryId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UploaderId");

                    b.HasIndex("UserId");

                    b.HasIndex("WallpaperGenreCategoryId");

                    b.ToTable("Wallpapers", (string)null);
                });

            modelBuilder.Entity("StarWall.Domain.WallpaperEntities.WallpaperDeviceCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("WallpaperDeviceCategories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Mobile"
                        },
                        new
                        {
                            Id = 2,
                            Title = "Desktop"
                        });
                });

            modelBuilder.Entity("StarWall.Domain.WallpaperEntities.WallpaperGenreCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("WallpaperGenreCategories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Title = "Movie"
                        },
                        new
                        {
                            Id = 2L,
                            Title = "Nature"
                        },
                        new
                        {
                            Id = 3L,
                            Title = "Games"
                        },
                        new
                        {
                            Id = 4L,
                            Title = "Anime"
                        },
                        new
                        {
                            Id = 5L,
                            Title = "Minimalist"
                        });
                });

            modelBuilder.Entity("StarWall.Domain.WallpaperEntities.WallpaperImage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WallpaperDeviceCategoryId")
                        .HasColumnType("int");

                    b.Property<long>("WallpaperId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("WallpaperDeviceCategoryId");

                    b.HasIndex("WallpaperId");

                    b.ToTable("WallpaperImages", (string)null);
                });

            modelBuilder.Entity("StarWall.Domain.WebsiteInfoEntities.WebSiteInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instagram")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LongDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber1")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("PhoneNumber2")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("PoweredBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telegram")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Twitter")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WebSiteInfos", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Iran - Tabriz - Valiasr Street",
                            Email = "starsite.iran@gmail.com",
                            Instagram = "StarWall_inst",
                            LongDescription = "Download amazing wallpapers from starwall and set it as your backgroun image on your devices.",
                            PhoneNumber1 = "09031813679",
                            PhoneNumber2 = "09031813679",
                            PoweredBy = "NetBold",
                            ShortDescription = "Wallpapers like Stars",
                            Telegram = "StarWall_Tel",
                            Title = "StarWall",
                            Twitter = "StarWall_Twi"
                        });
                });

            modelBuilder.Entity("StarWall.Domain.BlogEntities.Blog", b =>
                {
                    b.HasOne("StarWall.Domain.UserEntities.User", "Editor")
                        .WithMany()
                        .HasForeignKey("EditorId");

                    b.HasOne("StarWall.Domain.UserEntities.User", "Writer")
                        .WithMany("Blogs")
                        .HasForeignKey("WriterId");

                    b.Navigation("Editor");

                    b.Navigation("Writer");
                });

            modelBuilder.Entity("StarWall.Domain.UserEntities.User", b =>
                {
                    b.HasOne("StarWall.Domain.UserEntities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("StarWall.Domain.WallpaperEntities.Like", b =>
                {
                    b.HasOne("StarWall.Domain.UserEntities.User", "Liker")
                        .WithMany("Likes")
                        .HasForeignKey("LikerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("StarWall.Domain.WallpaperEntities.Wallpaper", "Wallpaper")
                        .WithMany("Likes")
                        .HasForeignKey("WallpaperId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Liker");

                    b.Navigation("Wallpaper");
                });

            modelBuilder.Entity("StarWall.Domain.WallpaperEntities.Wallpaper", b =>
                {
                    b.HasOne("StarWall.Domain.UserEntities.User", "Uploader")
                        .WithMany("UploadedWallpapers")
                        .HasForeignKey("UploaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StarWall.Domain.UserEntities.User", null)
                        .WithMany("DownloadedWallpapers")
                        .HasForeignKey("UserId");

                    b.HasOne("StarWall.Domain.WallpaperEntities.WallpaperGenreCategory", "WallpaperGenreCategory")
                        .WithMany("Wallpapers")
                        .HasForeignKey("WallpaperGenreCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Uploader");

                    b.Navigation("WallpaperGenreCategory");
                });

            modelBuilder.Entity("StarWall.Domain.WallpaperEntities.WallpaperImage", b =>
                {
                    b.HasOne("StarWall.Domain.WallpaperEntities.WallpaperDeviceCategory", "WallpaperDeviceCategory")
                        .WithMany("WallpaperImages")
                        .HasForeignKey("WallpaperDeviceCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StarWall.Domain.WallpaperEntities.Wallpaper", "Wallpaper")
                        .WithMany("WallpaperImages")
                        .HasForeignKey("WallpaperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wallpaper");

                    b.Navigation("WallpaperDeviceCategory");
                });

            modelBuilder.Entity("StarWall.Domain.UserEntities.User", b =>
                {
                    b.Navigation("Blogs");

                    b.Navigation("DownloadedWallpapers");

                    b.Navigation("Likes");

                    b.Navigation("UploadedWallpapers");
                });

            modelBuilder.Entity("StarWall.Domain.WallpaperEntities.Wallpaper", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("WallpaperImages");
                });

            modelBuilder.Entity("StarWall.Domain.WallpaperEntities.WallpaperDeviceCategory", b =>
                {
                    b.Navigation("WallpaperImages");
                });

            modelBuilder.Entity("StarWall.Domain.WallpaperEntities.WallpaperGenreCategory", b =>
                {
                    b.Navigation("Wallpapers");
                });
#pragma warning restore 612, 618
        }
    }
}
