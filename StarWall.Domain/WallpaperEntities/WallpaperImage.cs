using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWall.Domain.WallpaperEntities
{
    public class WallpaperImage
    {
        public long Id { get; set; }

        public string FileName { get; set; }

        public int WallpaperDeviceCategoryId { get; set; }
        public WallpaperDeviceCategory? WallpaperDeviceCategory { get; set; }

        public long WallpaperId { get; set; }
        public Wallpaper? Wallpaper { get; set; }
    }
}
