using CapstoneBE.Data;
using CapstoneBE.Helpers;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace CapstoneBE.Repositories.Users
{
    public class UserRepository : GenericRepository<CapstoneBEUser>, IUserRepository
    {
        public UserRepository(CapstoneDbContext capstoneDbContext, IOptions<AppSettings> appSettings,
            SignInManager<CapstoneBEUser> signInManager, UserManager<CapstoneBEUser> userManager) : base(capstoneDbContext)
        {
            AppSettings = appSettings.Value;
            SignInManager = signInManager;
            UserManager = userManager;
        }

        public AppSettings AppSettings { get; }

        public SignInManager<CapstoneBEUser> SignInManager { get; }

        public UserManager<CapstoneBEUser> UserManager { get; }

        public async Task DeleteUser(string userId)
        {
            CapstoneBEUser user = await GetById(userId);
            if (user != null)
                user.IsDel = true;
        }

        public async Task<CapstoneBEUser> GetByUserName(string userName)
        {
            CapstoneBEUser user = await UserManager.FindByNameAsync(userName);
            if (user == null || user.IsDel)
                return null;
            return user;
        }

        public async Task<IdentityResult> ResetPassword(string userId, string newPassword)
        {
            CapstoneBEUser user = await GetById(userId);
            if (user == null)
                throw new ArgumentNullException(nameof(userId));
            string token = await UserManager.GeneratePasswordResetTokenAsync(user);
            if (token == null)
                throw new Exception("Reset password token is invalid");
            IdentityResult result = await UserManager.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
                user.IsNewUser = true;
            return result;
        }

        public async Task<IdentityResult> ChangePasswordByToken(string userName, string newPassword, string token)
        {
            CapstoneBEUser user = await GetByUserName(userName);
            if (user == null)
                throw new ArgumentNullException(nameof(userName));
            if (token == null)
                throw new Exception("Change password token is invalid");
            IdentityResult result = await UserManager.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
                user.IsNewUser = false;
            return result;
        }

        public async Task UpdateBasicInfo(UserBasicInfo userBasicInfo, string userId)
        {
            CapstoneBEUser user = await GetById(userId);
            if (user != null)
            {
                if (!String.IsNullOrEmpty(userBasicInfo.Name))
                    user.Name = userBasicInfo.Name;
                if (!String.IsNullOrEmpty(userBasicInfo.PhoneNumber))
                    user.PhoneNumber = userBasicInfo.PhoneNumber;
                if (!String.IsNullOrEmpty(userBasicInfo.Address))
                    user.Address = userBasicInfo.Address;
                if (!String.IsNullOrEmpty(userBasicInfo.Email))
                    user.Email = userBasicInfo.Email;
            }
        }

        public override async Task<CapstoneBEUser> GetById(object id)
        {
            CapstoneBEUser user = await UserManager.FindByIdAsync((string)id);
            if (user == null || user.IsDel)
                return null;
            return user;
        }

        public async Task<IdentityResult> CreateUser(CapstoneBEUser newUser, string password)
        {
            if (newUser == null)
                throw new ArgumentNullException(nameof(newUser));
            IdentityResult createResult = await UserManager.CreateAsync(newUser, password);
            if (createResult.Succeeded)
            {
                IdentityResult roleResult = await UserManager.AddToRoleAsync(newUser, newUser.Role);
                if (!roleResult.Succeeded)
                    return roleResult;
            }
            return createResult;
        }

        public async Task<IdentityResult> ChangePassword(string oldPass, string newPass, string userId)
        {
            CapstoneBEUser user = await GetById(userId);
            if (user == null)
                throw new ArgumentNullException(nameof(userId));
            IdentityResult result = await UserManager.ChangePasswordAsync(user, oldPass, newPass);
            if (result.Succeeded)
                user.IsNewUser = false;
            return result;
        }

        public async Task UpdateFcmTokenM(string fcmToken, string userId)
        {
            CapstoneBEUser user = await GetById(userId);
            if (user != null)
                user.FcmTokenM = fcmToken;
        }

        public async Task UpdateFcmTokenW(string fcmToken, string userId)
        {
            CapstoneBEUser user = await GetById(userId);
            if (user != null)
                user.FcmTokenW = fcmToken;
        }
    }
}