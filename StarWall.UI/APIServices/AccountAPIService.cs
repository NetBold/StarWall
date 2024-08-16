using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using StarWall.Core.DTOs;
using StarWall.UI.Enums;
using StarWall.UI.Model;
using StarWall.UI.Pages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace StarWall.UI.APIServices
{
    public class AccountAPIService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IJSRuntime _jSRuntime;
        private HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigation;

        public AccountAPIService(IHttpClientFactory clientFactory, IJSRuntime jSRuntime, ILocalStorageService localStorage, NavigationManager navigation)
        {
            _clientFactory = clientFactory;
            _jSRuntime = jSRuntime;
            _client = _clientFactory.CreateClient("StarWallApi");
            _localStorage = localStorage;
            _navigation = navigation;
        }

        public async Task<bool> Register(RegisterUserDTO registerUserDTO,IBrowserFile? file)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            HttpResponseMessage regResponse = await _client.PostAsJsonAsync("api/Account/Register", registerUserDTO);
            var result = await regResponse.Content.ReadAsStringAsync();

            if (regResponse.IsSuccessStatusCode)
            {

                if (file != null)
                {
                    using var content = new MultipartFormDataContent();

                    var fileContent =
                                new StreamContent(file.OpenReadStream());

                    fileContent.Headers.ContentType =
                        new MediaTypeHeaderValue(file.ContentType);


                    content.Add(
                        content: fileContent,
                        name: "\"file\"",
                        fileName: file.Name);
                    HttpResponseMessage upResponse = await _client.PostAsync($"api/Account/UploadUserProfileFile?activeToken={result}", content);
                }

                LoginUserDTO loginUserDTO = new()
                {
                    Username = registerUserDTO.Username,
                    Password = registerUserDTO.Password
                };
                HttpResponseMessage activationResponse = await _client.PostAsJsonAsync($"api/Account/SendActivationMail", loginUserDTO);
                if (activationResponse.IsSuccessStatusCode)
                {
                    registerUserDTO = new();

                    await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), $"The activation TOKEN was sent to {registerUserDTO.Email}!");
                    string url = $"AccountActivation/{loginUserDTO.Username}";
                    _navigation.NavigateTo(url);
                    return true;
                }
                else
                {
                    await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString(), activationResponse.StatusCode);
                    return false;
                }
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString());
                return false;
            }
        }

        public async Task<bool> Login(LoginUserDTO loginUserDTO)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Account/Login", loginUserDTO);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync("authToken", result);
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), "Login Success!");
                _navigation.NavigateTo("/UserPanel");
                return true;
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString(), result);
                return false;
            }
        }

        public async Task RequestActivateAccount(LoginUserDTO loginUserDTO)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"api/Account/SendActivationMail", loginUserDTO);
            var result = await response.Content.ReadAsStringAsync();
            APIServiceResult serviceResult = new();
            if (response.IsSuccessStatusCode)
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), result);
                _navigation.NavigateTo($"/AccountActivation/{loginUserDTO.Username}");
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString(), result);
            }
        }

        public async Task<bool> ActivateAccount(AccountActivationDTO accountActivationDTO)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Account/ActivateAccount", accountActivationDTO);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {                
                accountActivationDTO = new();               
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), result);
                return true;
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString(), result);
                return false;
            }
        }

        public async Task<UserDTO> GetUserInfoForUserPanel()
        {
            UserDTO userDTO;
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Account/GetUser");
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                userDTO = await JsonSerializer.DeserializeAsync<UserDTO>(responseStream, options);
                return userDTO;
            }
            return null;
        }

        public async Task<UpdateUserDTO> GetUserForEditProfile()
        {
            UpdateUserDTO updateUserDTO = new();
            string token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new HttpRequestMessage(HttpMethod.Get, "api/Account/GetUserForUpdate");
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                updateUserDTO = await JsonSerializer.DeserializeAsync<UpdateUserDTO>(responseStream, options);
            }
            return updateUserDTO;
        }

        public async Task<bool> EditProfile(UpdateUserDTO updateUserDTO, IBrowserFile? file)
        {
            HttpResponseMessage updateResponse = await _client.PostAsJsonAsync("api/Account/UpdateUser", updateUserDTO);
            var result = await updateResponse.Content.ReadAsStringAsync();
            if (updateResponse.IsSuccessStatusCode)
            {
                if (file != null)
                {
                    using var content = new MultipartFormDataContent();

                    var fileContent =
                                new StreamContent(file.OpenReadStream());

                    fileContent.Headers.ContentType =
                        new MediaTypeHeaderValue(file.ContentType);


                    content.Add(
                        content: fileContent,
                        name: "\"file\"",
                        fileName: file.Name);
                    HttpResponseMessage upResponse = await _client.PostAsync($"api/Account/UpdateUserProfileFile?activeToken={result}", content);
                }
                await _localStorage.RemoveItemAsync("authToken");
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), "Changes was Saved! Login to your account");
                _navigation.NavigateTo("/login");
                return true;
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString());
                return false;
            }
        }

        public async Task<bool> ResetPasswordFromUserPanel(ResetPasswordDTOInUserPanel ResetPasswordDTO)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Account/ResetPasswordInUserPanel", ResetPasswordDTO);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await _localStorage.RemoveItemAsync("authToken");
                ResetPasswordDTO = new();
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), result);
                _navigation.NavigateTo($"/login");
                return true;
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString(), result);
                return false;
            }
        }

        public async Task SendUserPasswordToken(EmailDTO emailDTO)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Account/SendUserPasswordToken", emailDTO);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                _navigation.NavigateTo($"/ResetPassword/{emailDTO.Email}");
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.success.ToString(), result);
            }
            else
            {
                await _jSRuntime.InvokeVoidAsync("ShowToastr", ToastType.error.ToString(), result);
            }
        }
    }
}
