using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapstoneBE.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            List<IInstaller> installers = typeof(Startup).Assembly.ExportedTypes
                .Where(i => typeof(IInstaller).IsAssignableFrom(i) && !i.IsInterface && !i.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
            installers.ForEach(i => i.InstallServices(services, configuration));
        }
    }
}