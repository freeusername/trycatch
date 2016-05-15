using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebShop.DAL.Models;

namespace WebShop.DAL
{
    public class DataBaseContext : IdentityDbContext<IdentityUser>, IDatabaseContext
    {
        public DataBaseContext()
            : base("DataBaseContext")
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        public IDbSet<Client> Clients { get; set; }
        public IDbSet<RefreshToken> RefreshTokens { get; set; }
        public IDbSet<Article> Articles { get; set; }

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

            modelBuilder.Entity<Article>().ToTable("data.Articles");
        }
    }

    public interface IDatabaseContext
    {
        IDbSet<Client> Clients { get; set; }
        IDbSet<Article> Articles { get; set; }
        IDbSet<RefreshToken> RefreshTokens { get; set; }
        int SaveChanges();
    }
}