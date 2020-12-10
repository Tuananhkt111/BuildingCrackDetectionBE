using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CapstoneBE.Services.Users
{
    public interface IUserService
    {
        Task<UserLoginResponse> Authenticate(string userName, string password, string registrationToken = "");

        string GenerateJWTToken(string userId, string roleName, int[] locationIds);

        Task<UserInfo> GetUserById(string userId);

        List<UserInfo> GetUsers(Expression<Func<CapstoneBEUser, bool>> filter = null,
            Func<IQueryable<CapstoneBEUser>, IOrderedQueryable<CapstoneBEUser>> orderBy = null,
            string includeProperties = "", int limit = 0, int offset = 0);

        int GetUsersCount(Expression<Func<CapstoneBEUser, bool>> filter = null,
            Func<IQueryable<CapstoneBEUser>, IOrderedQueryable<CapstoneBEUser>> orderBy = null,
            string includeProperties = "", int limit = 0, int offset = 0);

        Task<bool> DeleteUser(string userId);

        Task<int> UpdateBasicInfo(UserBasicInfo userBasicInfo, string userId);

        Task<Email> ResetPassword(string userId);

        Task<Email> ResetPassword(string userId, string token);

        Task<Email> ForgotPassword(string userName);

        Task<bool> ChangePassword(string oldPass, string newPass, string userId);

        string GenerateRandomPasword(PasswordOptions opts = null);

        string GenerateUserName(string name);

        Task<Email> CreateUser(UserCreate newUser);

        Task<bool> ChangeRole(string roleName, string userId);
    }
}