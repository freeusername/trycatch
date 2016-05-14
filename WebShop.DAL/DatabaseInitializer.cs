using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebShop.DAL.Models;

namespace WebShop.DAL
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<AuthContext>
    {
        protected override void Seed(AuthContext context)
        {
            if (context.Clients.Any())
            {
                return;
            }

            var clients = BuildClientsList();
            foreach (var client in clients)
                context.Clients.Add(client);

            context.SaveChanges();
        }

        private static IEnumerable<Client> BuildClientsList()
        {
            var ClientsList = new List<Client>
            {
                new Client
                {
                    Id = "mvcApp",
                    Secret= Helper.GetHash("abc@123"),
                    Name="MVC application",
                    ApplicationType =  ApplicationTypes.MVC,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "*"
                },
                new Client
                {
                    Id = "consoleApp",
                    Secret=Helper.GetHash("123@abc"),
                    Name="Console Application",
                    ApplicationType =ApplicationTypes.WPF,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                }
            };

            return ClientsList;
        }
    }
}