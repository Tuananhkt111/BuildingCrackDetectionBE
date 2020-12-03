using CapstoneBE.Data;
using CapstoneBE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CapstoneBE.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CapstoneDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("CapstoneLocalContext")));
            services.AddIdentity<CapstoneBEUser, IdentityRole>()
                .AddEntityFrameworkStores<CapstoneDbContext>().AddDefaultTokenProviders();
        }
    }
}