using AutoMapper;
using StarWall.Core.DTOs;
using StarWall.Domain.ContactEntities;
using StarWall.Domain.UserEntities;
using StarWall.Domain.WallpaperEntities;
using StarWall.Domain.WebsiteInfoEntities;

namespace StarWall.Core.AutoMapperProfiles;

public class MapperInitializer : Profile
{
    public MapperInitializer()
    {
        #region WallpaperMapperInitializer
        CreateMap<Wallpaper, AdminPanelWallpaperDTO>().ReverseMap();
        CreateMap<Wallpaper, CreateWallpaperDTO>().ReverseMap();
        CreateMap<Wallpaper, ViewWallpaperDTO>().ReverseMap();
        CreateMap<Wallpaper, EditWallpaperDTO>().ReverseMap();
        #endregion

        #region UserMapperInitializer
        CreateMap<User, RegisterUserDTO > ().ReverseMap();
        CreateMap<User, LoginUserDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, UpdateUserDTO>().ReverseMap();
        #endregion

        #region ContactMapperInitializer
        CreateMap<Contact, ContactDTO>().ReverseMap();
        #endregion

        #region WebSiteInfoMapperInitializer
        CreateMap<WebSiteInfo, WebSiteInfoDTO>().ReverseMap();
        CreateMap<WebSiteInfo, UpdateWebSiteInfoDTO>().ReverseMap();
        #endregion

        #region GenreMapperInitializer
        CreateMap<WallpaperGenreCategory,CreateGenreDTO>().ReverseMap();
        CreateMap<WallpaperGenreCategory, UpdateGenreDTO>().ReverseMap();
        CreateMap<WallpaperGenreCategory, GenreDTO>().ReverseMap();
        #endregion
    }
}