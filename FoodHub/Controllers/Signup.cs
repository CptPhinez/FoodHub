using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using FoodHub.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
public class SignupModel : PageModel
{
    private readonly UserAccountService _userAccountService;
    private readonly IJSRuntime _js;
    private readonly CustomAuthenticationStateProvider _authStateProvider;
    private readonly NavigationManager _navManager;

    public SignupModel(UserAccountService userAccountService, IJSRuntime js, CustomAuthenticationStateProvider authStateProvider,
       NavigationManager navManager)
    {
        _userAccountService = userAccountService;
        _js = js;
        _authStateProvider = authStateProvider;
        _navManager = navManager;
    }

    [BindProperty]
    public UserModel Model { get; set; } = new UserModel();

    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

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


