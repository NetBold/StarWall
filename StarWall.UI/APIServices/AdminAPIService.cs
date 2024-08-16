using Azure.Core;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using StarWall.Core.DTOs;
using StarWall.Core.Models;
using StarWall.Domain.WallpaperEntities;
using StarWall.UI.Enums;
using System.Net.Http.Headers;
using System.Text.Json;

namespace StarWall.UI.APIServices
{
    public class AdminAPIService
    {
        private HttpClient _client;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IJSRuntime _jSRuntime;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigation;

        public AdminAPIService(IHttpClientFactory clientFactory, IJSRuntime jSRuntime, ILocalStorageService localStorage, NavigationManager navigation)
        {
            _clientFactory = clientFactory;
            _jSRuntime = jSRuntime;
            _localStorage = localStorage;
            _navigation = navigation;
            _client = _clientFactory.CreateClient("StarWallApi");
        }


        public async Task<UpdateWebSiteInfoDTO> GetWebInfoForEdit()
        {
            UpdateWebSiteInfoDTO webSiteInfoDTO;
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Admin/GetWebInfoForEdit");
            var response = await _client.SendAsync(request);
            if(response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                webSiteInfoDTO = await JsonSerializer.DeserializeAsync<UpdateWebSiteInfoDTO>(responseStream, options);
                return webSiteInfoDTO;
            }
            return null;
        }

        public async Task UpdateWebInfo(UpdateWebSiteInfoDTO webSiteInfoDTO)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await _client.PutAsJsonAsync($"api/Admin/UpdateWebInfo?id={webSiteInfoDTO.Id}",webSiteInfoDTO);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), responseString);
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString(), responseString);
            }
        }

        public async Task<List<ContactDTO>> GetUnReadMessages()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Admin/GetUnReadMessages");
            var response = await _client.SendAsync(request);
            if(response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = await JsonSerializer.DeserializeAsync<List<ContactDTO>>(responseStream, options);
                return result;
            }
            return null;
        }

        public async Task<List<ContactDTO>> GetReadMessages()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Admin/GetReadMessages");
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = await JsonSerializer.DeserializeAsync<List<ContactDTO>>(responseStream, options);
                return result;
            }
            return null;
        }

        public async Task DeleteContact(long id)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Admin/DeleteUnReadMessage/{id}");
            var response = await _client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), responseString);
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString(), responseString);
            }
        }

        public async Task<ContactDTO> GetContactMessage(long id)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Admin/GetContactMessage/{id}");
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = await JsonSerializer.DeserializeAsync<ContactDTO>(responseStream, options);
                return result;
            }
            return null;
        }

        public async Task MakeContactMessageSeen(long id)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/Admin/MakeMessageSeen/{id}");
            var response = await _client.SendAsync(request);
        }

        public async Task<List<AdminPanelWallpaperDTO>> GetWallpapers()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Admin/GetWallpapersForAdminPanel");
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = await JsonSerializer.DeserializeAsync<List<AdminPanelWallpaperDTO>>(responseStream, options);
                return result;
            }
            return null;
        }

        public async Task DeleteWallpaper(long id)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Admin/DeleteWallpaperById/?id={id}");
            var response = await _client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), responseString);
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString(), responseString);
            }
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

        public async Task CreateWallpaper(CreateWallpaperDTO createWallpaperDTO, IBrowserFile desktopFile, IBrowserFile mobileFile)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Admin/CreateWallpaper", createWallpaperDTO);
            var result = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                if (desktopFile != null)
                {
                    using var content = new MultipartFormDataContent();

                    var fileContent =
                                new StreamContent(desktopFile.OpenReadStream(maxAllowedSize:long.MaxValue));

                    fileContent.Headers.ContentType =
                        new MediaTypeHeaderValue(desktopFile.ContentType);


                    content.Add(
                        content: fileContent,
                        name: "\"file\"",
                        fileName: desktopFile.Name);
                    HttpResponseMessage upResponse = await _client.PostAsync($"api/Admin/UploadWallpaperFile/{Convert.ToInt64(result)}/2", content);
                }
                if (mobileFile != null)
                {
                    using var content = new MultipartFormDataContent();

                    var fileContent =
                                new StreamContent(mobileFile.OpenReadStream(maxAllowedSize: long.MaxValue));

                    fileContent.Headers.ContentType =
                        new MediaTypeHeaderValue(mobileFile.ContentType);


                    content.Add(
                        content: fileContent,
                        name: "\"file\"",
                        fileName: mobileFile.Name);
                    HttpResponseMessage upResponse = await _client.PostAsync($"api/Admin/UploadWallpaperFile/{Convert.ToInt64(result)}/1", content);
                }
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), "Created Successfully");
            }
        }

        public async Task<EditWallpaperDTO> GetWallpaperForEdit(long id)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Admin/GetWallpaperForEdit/{id}");
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = await JsonSerializer.DeserializeAsync<EditWallpaperDTO>(responseStream, options);
                return result;
            }
            return null;
        }

        public async Task EditWallpaper(EditWallpaperDTO editWallpaperDTO, IBrowserFile desktopFile, IBrowserFile mobileFile,long id)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/Admin/UpdateWallpaperById/{id}", editWallpaperDTO);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                if (desktopFile != null)
                {
                    using var content = new MultipartFormDataContent();

                    var fileContent =
                                new StreamContent(desktopFile.OpenReadStream(maxAllowedSize: long.MaxValue));

                    fileContent.Headers.ContentType =
                        new MediaTypeHeaderValue(desktopFile.ContentType);


                    content.Add(
                        content: fileContent,
                        name: "\"file\"",
                        fileName: desktopFile.Name);
                    HttpResponseMessage upResponse = await _client.PostAsync($"api/Admin/UploadWallpaperFile/{id}/2", content);
                }
                if (mobileFile != null)
                {
                    using var content = new MultipartFormDataContent();

                    var fileContent =
                                new StreamContent(mobileFile.OpenReadStream(maxAllowedSize: long.MaxValue));

                    fileContent.Headers.ContentType =
                        new MediaTypeHeaderValue(mobileFile.ContentType);


                    content.Add(
                        content: fileContent,
                        name: "\"file\"",
                        fileName: mobileFile.Name);
                    HttpResponseMessage upResponse = await _client.PostAsync($"api/Admin/UploadWallpaperFile/{id}/1", content);
                }
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), "Created Successfully");
            }
        }

        public async Task DeleteGenre(long id)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Admin/DeleteWallpaperGenreById/{id}");
            var response = await _client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), responseString);
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString(), responseString);
            }
        }

        public async Task CreateGenre(CreateGenreDTO createGenreDTO)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Admin/CreateGenre", createGenreDTO);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {                
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), "Created Successfully");
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString());
            }
        }

        public async Task<UpdateGenreDTO> GetGenreForUpdate(long id)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Admin/GetGenreForUpdate/{id}");
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = await JsonSerializer.DeserializeAsync<UpdateGenreDTO>(responseStream, options);
                return result;
            }
            return null;
        }

        public async Task UpdateGenre(long id,UpdateGenreDTO updateGenreDTO)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/Admin/UpdateGenre/{id}", updateGenreDTO);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), "Created Successfully");
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString());
            }
        }
    }
}
