using CapstoneBE.Services.Cracks;
using CapstoneBE.Services.Emails;
using CapstoneBE.Services.Locations;
using CapstoneBE.Services.MaintenanceWorkers;
using CapstoneBE.Services.Users;
using CapstoneBE.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CapstoneBE.Installers
{
    public class ScopedServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMaintenanceWorkerService, MaintenanceWorkerService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICrackService, CrackService>();
        }
    }
}