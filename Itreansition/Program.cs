using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Dropbox.Api;

namespace Itreansition
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {


                    var userManager = services.GetRequiredService<UserManager<Models.User>>();
                    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await Initialize.StartInitialize.InitializeAsync(userManager, rolesManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            //using (var dbx = new DropboxClient("A6kzjrFpoIAAAAAAAAAAg1GCoCwaGRyt4aUVoXPWhbYZKIvKOzb0JI8dMqAm4YVG"))
            //{
            //    var full = await dbx.Users.GetCurrentAccountAsync();
            //    Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
            //}

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
