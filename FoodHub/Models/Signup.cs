using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using FoodHub.Entities;
using FoodHub.Services;
using FoodHub.Models;

namespace FoodHub.Pages
{
    public class SignupModel : PageModel
    {
        private readonly UserAccountService _userAccountService;
        private readonly IJSRuntime _js;
        private readonly CustomAuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navManager;

        public SignupModel(UserAccountService userAccountService, IJSRuntime js, CustomAuthenticationStateProvider authStateProvider, NavigationManager navManager)
        {
            _userAccountService = userAccountService;
            _js = js;
            _authStateProvider = authStateProvider;
            _navManager = navManager;
        }

        [BindProperty]
        public UserModel Model { get; set; } = new UserModel();

        public async Task<IActionResult> OnPostAsync()
        {
            if (Model.Password != Model.ConfirmPassword)
            {
                await _js.InvokeVoidAsync("alert", "Passwords do not match");
                return Page();
            }

            var existingUser = _userAccountService.GetUserName(Model.UserName);
            if (existingUser != null)
            {
                await _js.InvokeVoidAsync("alert", "Username already exists");
                return Page();
            }

            var newUserAccount = new UserAccount
            {
                UserName = Model.UserName,
                Password = Model.Password,
                Role = "NewUser"
            };

            _userAccountService.CreateUser(newUserAccount);

            await _authStateProvider.UpdateAuthenticationState(new UserSession
            {
                UserName = newUserAccount.UserName,
                Role = newUserAccount.Role
            });

            _navManager.NavigateTo("/", true);
            return RedirectToPage("/Index");
        }
    }
}
