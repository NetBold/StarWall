using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using StarWall.Core.Models;
using StarWall.Core.Security;
using StarWall.UI.Enums;

public class CustomAuthStateHandler
{
    private readonly ILocalStorageService localStorage;
    private readonly NavigationManager Navigation;

    public CustomAuthStateHandler(ILocalStorageService localStorage, NavigationManager navigation)
    {
        this.localStorage = localStorage;
        Navigation = navigation;
    }

    private async Task<string> GetToken()
    {
        string token = await localStorage.GetItemAsync<string>("authToken");
        return token;
    }

    private void NavToLogin() => Navigation.NavigateTo("/login");

    public async Task<DecodedJWTModel> CheckAuthorizationNormalUserAndGetDecoded()
    {
        var token = await GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            var tokenInfo = JWTHelper.GetDecodedJWTModelByToken(token);
            if (string.IsNullOrEmpty(tokenInfo.Username) || tokenInfo == null)
            {
                NavToLogin();
            }
            else
            {
                return tokenInfo;
            }
        }
        else
        {
            NavToLogin();
        }
        return null;
    }

    public async Task<DecodedJWTModel> IsAuthorizationNormalUserAndGetDecoded()
    {
        var token = await GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            var tokenInfo = JWTHelper.GetDecodedJWTModelByToken(token);
            if (string.IsNullOrEmpty(tokenInfo.Username) || tokenInfo == null)
            {
                
            }
            else
            {
                return tokenInfo;
            }
        }
        else
        {
            
        }
        return null;
    }

    public async Task<string> CheckAuthorizationNormalUser()
    {
        var token = await GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            var tokenInfo = JWTHelper.GetDecodedJWTModelByToken(token);
            if (string.IsNullOrEmpty(tokenInfo.Username) || tokenInfo == null)
            {
                NavToLogin();
            }
            else
            {
                return token;
            }
        }
        else
        {
            NavToLogin();
        }
        return "";
    }

    public async Task<string> CheckAuthorizationAdmin()
    {
        var token = await GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            var tokenInfo = JWTHelper.GetDecodedJWTModelByToken(token);
            if (string.IsNullOrEmpty(tokenInfo.Username) || tokenInfo.Role != "Admin" || tokenInfo == null)
            {
                NavToLogin();
            }
            else
            {
                return token;
            }
        }
        else
        {
            NavToLogin();
        }
        return "";
    }

    public async Task<bool> IsAuthorizationAdmin()
    {
        var token = await GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            var tokenInfo = JWTHelper.GetDecodedJWTModelByToken(token);
            if (string.IsNullOrEmpty(tokenInfo.Username) || tokenInfo.Role != "Admin" || tokenInfo == null)
                return false;
            else
                return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<string> CheckAuthorizationAdminOrUploader()
    {
        var token = await GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            var tokenInfo = JWTHelper.GetDecodedJWTModelByToken(token);
            if (string.IsNullOrEmpty(tokenInfo.Username) || tokenInfo == null)
            {
                NavToLogin();
            }
            else
            {
                if (tokenInfo.Role == "Admin" || tokenInfo.Role == "Uploader")
                {
                    return token;
                }
                else
                {
                    NavToLogin();
                }
            }
        }
        else
        {
            NavToLogin();
        }
        return "";
    }

    public async Task<bool> IsAuthorizationAdminOrUploader()
    {
        var token = await GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            var tokenInfo = JWTHelper.GetDecodedJWTModelByToken(token);
            if (string.IsNullOrEmpty(tokenInfo.Username) || tokenInfo == null)
            {
                
            }
            else
            {
                if (tokenInfo.Role == "Admin" || tokenInfo.Role == "Uploader")
                {
                    return true;
                }
                else
                {
                    
                }
            }
        }
        else
        {
            
        }
        return false;
    }

    public async Task<string> GetUserRole()
    {
        var token = await GetToken();
        if (!string.IsNullOrEmpty(token))
        {
            var tokenInfo = JWTHelper.GetDecodedJWTModelByToken(token);
            return tokenInfo.Role;
        }
        else
        {
            return null;
        }
    }

    public async Task Logout()
    {
        await localStorage.RemoveItemAsync("authToken");
        Navigation.NavigateTo($"/login");
    }
}