using StarWall.Domain.UserEntities;
using StarWall.Domain.WallpaperEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWall.Core.DTOs
{
    public class AdminPanelWallpaperDTO
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public long DownloadsCount { get; set; }
        public long ViewsCount { get; set; }

        public long UploaderId { get; set; }
        public User Uploader { get; set; }

        public int WallpaperGenreCategoryId { get; set; }
        public WallpaperGenreCategory? WallpaperGenreCategory { get; set; }

        public List<Like> Likes { get; set; }

        public List<WallpaperImage> WallpaperImages { get; set; }

        public DateTime PublishDate { get; set; }
    }

    public class CreateWallpaperDTO
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage ="Enter {0}")]
        [MaxLength(120,ErrorMessage ="{0} can not be more than {1} characters")]
        public string? Title { get; set; }

        [Display(Name = "Wallpaper Genre")]
        [Required(ErrorMessage = "Enter {0}")]
        public string WallpaperGenreCategoryTitle { get; set; }

        [Display(Name = "Source")]
        public string Source { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Enter {0}")]
        public string Description { get; set; }

        public long UploaderId { get; set; }
    }

    public class EditWallpaperDTO
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Enter {0}")]
        [MaxLength(120, ErrorMessage = "{0} can not be more than {1} characters")]
        public string? Title { get; set; }

        [Display(Name = "Wallpaper Genre")]
        [Required(ErrorMessage = "Enter {0}")]
        public string WallpaperGenreCategoryTitle { get; set; }

        [Display(Name = "Source")]
        public string Source { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Enter {0}")]
        public string Description { get; set; }

        public string DesktopImagePath { get; set; }
        public bool IsDesktopImageUpdated { get; set; }

        public string MobileImagePath { get; set; }
        public bool IsMobileImageUpdated { get; set; }
    }

    public class ViewWallpaperDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long DownloadsCount { get; set; }
        public long ViewsCount { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }

        public User? Uploader { get; set; }

        public WallpaperGenreCategory? WallpaperGenreCategory { get; set; }

        public virtual List<Like>? Likes { get; set; }

        public virtual List<WallpaperImage>? WallpaperImages { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
