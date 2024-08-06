using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using FoodHub.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
public class LoginModel : PageModel
{
	private readonly UserAccountService _userAccountService;
	private readonly IJSRuntime _js;
	private readonly AuthenticationStateProvider _authStateProvider;
	private readonly NavigationManager _navManager;
	public LoginModel(UserAccountService userAccountService, IJSRuntime js, AuthenticationStateProvider authStateProvider,
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
	}
	public async Task<IActionResult> OnPostAsync()
	{
		var userAccount = _userAccountService.GetUserName(Model.UserName);
		if (userAccount == null || userAccount.Password != Model.Password)
		{
			await _js.InvokeVoidAsync("alert", "Invalid Username or Password");
			return Page();
		}
		var customAuthStateProvider = (CustomAuthenticationStateProvider)_authStateProvider;
		await customAuthStateProvider.UpdateAuthenticationState(new UserSession
		{
			UserName = userAccount.UserName,
			Role = userAccount.Role
		});
		_navManager.NavigateTo("/", true);
		return RedirectToPage("/Index");
	}
}
