using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using WebShop.Entities;

namespace WebShop
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("auth.Users");
            modelBuilder.Entity<IdentityUserRole>().ToTable("auth.UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("auth.UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("auth.UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("auth.Roles");

            modelBuilder.Entity<Client>().ToTable("auth.Clients");
            modelBuilder.Entity<RefreshToken>().ToTable("auth.RefreshTokens");
        }
    }

    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<AuthContext>
    {
        protected override void Seed(AuthContext context)
        {
            if (context.Clients.Any())
            {
                return;
            }

            context.Clients.AddRange(BuildClientsList());
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
                    ApplicationType =  Models.ApplicationTypes.MVC,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "*"
                },
                new Client
                {
                    Id = "consoleApp",
                    Secret=Helper.GetHash("123@abc"),
                    Name="Console Application",
                    ApplicationType =Models.ApplicationTypes.WPF,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                }
            };

            return ClientsList;
        }
    }
}