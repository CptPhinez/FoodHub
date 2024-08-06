using System.Collections.Generic;
using System.Linq;
namespace FoodHub.Services
{
    public class UserAccountService
    {
        private readonly List<UserAccount> _userAccounts;
        public UserAccountService()
        {
            // Dito gagawa tayo ng dummy data pero di pa naman siya gaanong need since wala pa naman sa database 
            _userAccounts = new List<UserAccount>
 {
 new UserAccount { UserName = "admin", Password = "admin", Role = "Admin" },
 new UserAccount { UserName = "user", Password = "user", Role = "User" }
 };
        }
        public UserAccount GetUserName(string userName)
        {
            return _userAccounts.FirstOrDefault(u => u.UserName == userName);
        }
        public void CreateUser(UserAccount newUser)
        {
            _userAccounts.Add(newUser);
        }
    }
    //Eto mga sample models pwede mo yan ilipat sa models folder pero double check yung namespace kasi baka hindi ma read 
    public class UserAccount
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class UserSession
    {
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
