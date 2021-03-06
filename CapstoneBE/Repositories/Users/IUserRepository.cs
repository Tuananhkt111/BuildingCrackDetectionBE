using CapstoneBE.Helpers;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.Users
{
    public interface IUserRepository
    {
        public AppSettings AppSettings { get; }
        public SignInManager<CapstoneBEUser> SignInManager { get; }
        public UserManager<CapstoneBEUser> UserManager { get; }

        Task<IdentityResult> CreateUser(CapstoneBEUser newUser, string password);

        Task<IdentityResult> ResetPassword(string userId, string newPassword);

        Task<IdentityResult> ChangePasswordByToken(string userName, string newPassword, string token);

        Task<IdentityResult> ChangePassword(string oldPass, string newPass, string userId);

        Task<CapstoneBEUser> GetByUserName(string userName);

        Task DeleteUser(string userId);

        Task UpdateFcmTokenM(string registrationToken, string userId);
        Task UpdateFcmTokenW(string registrationToken, string userId);

        Task UpdateBasicInfo(UserBasicInfo userBasicInfo, string userId);
    }
}