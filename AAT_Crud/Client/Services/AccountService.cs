using Blazored.LocalStorage;
using Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedClasses.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace Client.Services
{
    public class AccountService : IAccountService
    {
        [Inject]
        public ISnackbar _snackbar { get; set; }

        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navMan;
        private readonly ILocalStorageService _localStorage;
        public List<UserDTO> Employees { get; set; } = new List<UserDTO>();
        public List<UserDTO> AppUsers { get; set; } = new List<UserDTO>();
        public List<LoginDTO> LoginUsers { get; set; } = new List<LoginDTO>();
        public List<char> ApiLoginResponse { get; set; } = new List<char>();
        public HttpResponseMessage ApiResponse { get; set; } = new HttpResponseMessage();

        public AccountService(HttpClient httpClient, NavigationManager NavMan, ILocalStorageService localStorage, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _navMan = NavMan;
            _localStorage = localStorage;
            _snackbar = snackbar;
        }

        #region change password
        public async Task ChangePassword(LoginDTO loginDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/AppUser/ChangePassword/", loginDTO);
            var response = result.StatusCode;

            if (response == System.Net.HttpStatusCode.Accepted)
            {
                _snackbar.Add("Password changed sucessfully", Severity.Success, config => { config.ShowCloseIcon = false; });
            }
            else
            {
                _snackbar.Add(response.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
                _navMan.NavigateTo("EditProfile", true);
            }
        }
        #endregion

        #region forgot password
        public async Task ForgotPassword(LoginDTO loginDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/AppUser/ForgotPassword/", loginDTO);
            var response = result.StatusCode;

            if (response == System.Net.HttpStatusCode.Accepted)
            {
                _snackbar.Add("Password reset successfully", Severity.Success, config => { config.ShowCloseIcon = false; });
                _navMan.NavigateTo("/", true);
            }
            else
            {
                _snackbar.Add(response.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
                _navMan.NavigateTo("ForgotPasswordChange", true);
            }
        }

        public async Task FindEmail(string email)
        {
            var result = await _httpClient.PostAsJsonAsync("api/AppUser/FindEmail", email);
            var response = result.StatusCode;

            if (response == System.Net.HttpStatusCode.Accepted)
            {
                _navMan.NavigateTo("ForgotPasswordChange", true);
                await _localStorage.SetItemAsync("EmailForgotPassWord", email);
            }
            else
            {
                _navMan.NavigateTo("/", true);
                _snackbar.Add("Email has been sent with directions to update password", Severity.Success, config => { config.ShowCloseIcon = false; });
            }
        }
        #endregion

        #region Register new account
        public async Task Register(UserDTO appUser)
        {
            var result = await _httpClient.PostAsJsonAsync("api/AppUser/Register", appUser);

            var response = result.StatusCode;
            if (response != System.Net.HttpStatusCode.Accepted)
            {
                _snackbar.Add(response.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
            }
            if (response == System.Net.HttpStatusCode.Accepted)
            {
                var data = await result.Content.ReadFromJsonAsync<List<UserDTO>>();
                _navMan.NavigateTo("/");
            }
            _snackbar.Add(response.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
        }
        #endregion

        #region Login
        public async Task Login(LoginDTO loginDTO)
        {
            var result = await _httpClient.PostAsJsonAsync("api/AppUser/Login", loginDTO);
            var response = result.StatusCode;
            if (response != System.Net.HttpStatusCode.Accepted)
            {
                _snackbar.Add("Username or password is incorrect", Severity.Error, config => { config.ShowCloseIcon = false; });
            }
            if (response == System.Net.HttpStatusCode.MethodNotAllowed)
            {
                _snackbar.Add(response.ToString() + " Please Confirm Email Address ", Severity.Warning, config => { config.ShowCloseIcon = false; });
            }
            if (response == System.Net.HttpStatusCode.Accepted)
            {
                _snackbar.Add("Welcome", Severity.Success, config => { config.ShowCloseIcon = false; });
                _navMan.NavigateTo("Dashboard", true);
                //await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "UserName", loginDTO.Email);
                await _localStorage.SetItemAsync("UserName", loginDTO.Email);
            }

        }
        #endregion

        #region Get Admin Details
        public async Task<UserDTO> GetUserByEmailTest(string email)
        {
            var result = await _httpClient.GetFromJsonAsync<UserDTO>($"api/AppUser/GetUserByEmail/" + email);
            return result;
        }
        #endregion
    }
}
