using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using StarWall.Core.DTOs;
using StarWall.Domain.WallpaperEntities;
using StarWall.UI.Enums;
using System.Net.Http.Headers;
using System.Text.Json;

namespace StarWall.UI.APIServices
{
    public class HomeAPIService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IJSRuntime _jSRuntime;
        private HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigation;

        public HomeAPIService(IHttpClientFactory clientFactory, IJSRuntime jSRuntime, ILocalStorageService localStorage, NavigationManager navigation)
        {
            _clientFactory = clientFactory;
            _jSRuntime = jSRuntime;
            _client = _clientFactory.CreateClient("StarWallApi");
            _localStorage = localStorage;
            _navigation = navigation;
        }

        public async Task SendContactMessage(ContactDTO contactDTO)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Contact/SendMessage", contactDTO);
            if (response.IsSuccessStatusCode)
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), "Message sent!");
                contactDTO = new();
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString(), "error");
            }
        }

        public async Task<WebSiteInfoDTO> GetWebSiteInfo()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Home/GetWebsiteInfo");
            var response = await _client.SendAsync(request);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return await JsonSerializer.DeserializeAsync<WebSiteInfoDTO>(responseStream, options);
        }

        public async Task<List<WallpaperGenreCategory>> GetWallpaperGenreCategories()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Admin/GetWallpaperGenreCategories");
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = await JsonSerializer.DeserializeAsync<List<WallpaperGenreCategory>>(responseStream, options);
                return result;
            }
            return null;
        }

        public async Task<ViewWallpaperDTO> GetWallpaperForView(long id, bool seen)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Wallpaper/GetWallpaperForView/{id}/{seen}");
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = await JsonSerializer.DeserializeAsync<ViewWallpaperDTO>(responseStream, options);
                return result;
            }
            return null;
        }

        public async Task LikeWallpaper(long id)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Wallpaper/LikeWallpaper/{id}");
            var response = await _client.SendAsync(request);
        }

        public async Task<List<WallpaperDeviceCategory>> GetWallpaperDeviceCategories()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Admin/GetWallpaperDeviceCategories");
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = await JsonSerializer.DeserializeAsync<List<WallpaperDeviceCategory>>(responseStream, options);
                return result;
            }
            return null;
        }

        public async Task<string> DownloadWallpaper(long id, string device)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Wallpaper/DownloadWallpaper/{id}?device={device}");
            var response = await _client.SendAsync(request);
            string result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {               
                return result;
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString(), result);
            }
            return null;
        }

        public async Task<List<ViewWallpaperDTO>> GetAllWallpapers()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Wallpaper/GetAllWallpapers/");
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = await JsonSerializer.DeserializeAsync<List<ViewWallpaperDTO>>(responseStream, options); 
                return result;
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString(), "error");
            }
            return null;
        }
    }
}
