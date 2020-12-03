using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CapstoneBE.Installers
{
    public interface IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}