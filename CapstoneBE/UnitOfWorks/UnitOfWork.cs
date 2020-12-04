using CapstoneBE.Data;
using CapstoneBE.Helpers;
using CapstoneBE.Models;
using CapstoneBE.Repositories.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CapstoneBE.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CapstoneDbContext _capstoneDbContext;
        private readonly UserManager<CapstoneBEUser> _userManager;
        private readonly SignInManager<CapstoneBEUser> _signInManager;
        private readonly IOptions<AppSettings> _appSettings;
        private UserRepository _userRepository;

        public UnitOfWork(CapstoneDbContext capstoneDbContext, UserManager<CapstoneBEUser> userManager,
            SignInManager<CapstoneBEUser> signInManager, IOptions<AppSettings> appSettings)
        {
            _capstoneDbContext = capstoneDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings;
        }

        public UserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_capstoneDbContext, _appSettings, _signInManager, _userManager);
                return _userRepository;
            }
        }

        public IDbContextTransaction GetTransaction()
        {
            return _capstoneDbContext.Database.BeginTransaction();
        }

        public async Task<int> Save()
        {
            return await _capstoneDbContext.SaveChangesAsync();
        }
    }
}