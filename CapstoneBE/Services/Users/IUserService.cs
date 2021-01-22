using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Users;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CapstoneBE.Services.Users
{
    public interface IUserService
    {
        Task<UserLoginResponse> Authenticate(string userName, string password, string registrationToken = "", bool isManager = false);

        string GenerateJWTToken(string userId, string roleName, int[] locationIds);

        Task<UserInfo> GetUserById(string userId);

        List<UserInfo> GetUsers();

        int GetUsersCount();

        Task<bool> DeleteUser(string userId);

        Task<int> UpdateBasicInfo(UserBasicInfo userBasicInfo, UserInfo user);

        Task<Email> ResetPassword(string userId);

        Task<Email> ResetPassword(string userName, string token);

        Task<Email> ForgotPassword(string userName, string rootPath);

        Task<bool> ChangePassword(string oldPass, string newPass, string userId);

        string GenerateRandomPasword(PasswordOptions opts = null);

        string GenerateUserName(string name);

        string GetManagerIdByLocationId(int locationId);

        Task<Email> CreateUser(UserCreate newUser);
    }
}