using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsApi.Persistence;

namespace IsApi.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddAppConfiguration(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var seeder = provider.GetRequiredService<IdentityDbSeed>();
                seeder.Seed();
            }
        }
    }
}