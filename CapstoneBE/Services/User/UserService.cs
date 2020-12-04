using AutoMapper;
using CapstoneBE.Models;
using CapstoneBE.Models.Custom.Users;
using CapstoneBE.UnitOfWorks;
using CapstoneBE.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneBE.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserLoginResponse> Authenticate(string userName, string password, string registrationToken = "")
        {
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(userName))
                return null;
            SignInResult signInResult = await _unitOfWork.UserRepository.SignInManager.PasswordSignInAsync(userName, password, false, false);
            if (!signInResult.Succeeded)
                return null;
            CapstoneBEUser user = await _unitOfWork.UserRepository.GetByUserName(userName);
            string role = (await _unitOfWork.UserRepository.UserManager.GetRolesAsync(user)).FirstOrDefault();
            if (user != null && !user.IsDel && role != null)
            {
                //authentication successful so generate jwt token
                string token = GenerateJWTToken(user.Id, role);
                return new UserLoginResponse
                {
                    UserId = user.Id,
                    JwtToken = token,
                    IsNewUser = user.IsNewUser
                };
            }
            else return null;
        }

        public async Task<bool> ChangePassword(string oldPass, string newPass, string userId)
        {
            IdentityResult result = await _unitOfWork.UserRepository.ChangePassword(oldPass, newPass, userId);
            if (result.Succeeded)
                return await _unitOfWork.Save() != 0;
            return false;
        }

        public async Task<bool> ChangeRole(string roleName, string userId)
        {
            IdentityResult result = await _unitOfWork.UserRepository.UpdateRole(roleName, userId);
            if (result.Succeeded)
                return await _unitOfWork.Save() != 0;
            return false;
        }

        public async Task<bool> CreateUser(UserCreate newUser, string password)
        {
            CapstoneBEUser user = _mapper.Map<CapstoneBEUser>(newUser);
            user.UserName = GenerateUserName(user.Name);
            IdentityResult result = await _unitOfWork.UserRepository.CreateUser(user, password);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            await _unitOfWork.UserRepository.DeleteUser(userId);
            return await _unitOfWork.Save() != 0;
        }

        public string GenerateJWTToken(string userId, string roleName)
        {
            // authentication successful so generate jwt token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(ClaimTypes.Role, roleName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_unitOfWork.UserRepository.AppSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_unitOfWork.UserRepository.AppSettings.Issuer,
                _unitOfWork.UserRepository.AppSettings.Audience, claims, signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRandomPasword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 7,
                RequiredUniqueChars = 3,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = false,
                RequireUppercase = true
            };
            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789"                    // digits
            };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();
            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);
            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);
            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);
            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);
            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }
            return new string(chars.ToArray());
        }

        public string GenerateUserName(string name)
        {
            string[] names = name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string result = names[^1];
            for (int i = 0; i < names.Length - 1; i++)
            {
                result += names[i].ToUpper().Substring(0, 1);
            }
            List<string> temp = _unitOfWork.UserRepository
                .Get().Select(u => u.UserName.Replace(result, "")).ToList();
            int count = 0;
            foreach (string item in temp)
            {
                if (item.Equals("") || int.TryParse(item, out _))
                    count++;
            }
            return count > 0 ? (result + count) : result;
        }

        public async Task<UserInfo> GetUserById(string userId)
        {
            CapstoneBEUser user = await _unitOfWork.UserRepository.GetSingle(filter: u => u.Id.Equals(userId));
            return _mapper.Map<UserInfo>(user);
        }

        public List<UserInfo> GetUsers(Expression<Func<CapstoneBEUser, bool>> filter = null,
            Func<IQueryable<CapstoneBEUser>, IOrderedQueryable<CapstoneBEUser>> orderBy = null,
            string includeProperties = "", int limit = 0, int offset = 0)
        {
            return _unitOfWork.UserRepository.Get().Select(u => _mapper.Map<UserInfo>(u)).ToList();
        }

        public int GetUsersCount(Expression<Func<CapstoneBEUser, bool>> filter = null,
            Func<IQueryable<CapstoneBEUser>, IOrderedQueryable<CapstoneBEUser>> orderBy = null,
            string includeProperties = "", int limit = 0, int offset = 0)
        {
            return _unitOfWork.UserRepository.Get().Select(u => _mapper.Map<UserInfo>(u)).Count();
        }

        public async Task<bool> ResetPassword(string userId)
        {
            string newPass = GenerateRandomPasword();
            IdentityResult result = await _unitOfWork.UserRepository.ResetPassword(userId, newPass);
            if (result.Succeeded)
                return await _unitOfWork.Save() != 0;
            return false;
        }

        public async Task<int> UpdateBasicInfo(UserBasicInfo userBasicInfo, string userId)
        {
            await _unitOfWork.UserRepository.UpdateBasicInfo(userBasicInfo, userId);
            return await _unitOfWork.Save();
        }
    }
}