using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebShop.DAL.Models;

namespace WebShop.DAL
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            if (!context.Clients.Any())
            {
                var clients = BuildClientsList();
                foreach (var client in clients)
                    context.Clients.Add(client);
            }

            if (!context.Articles.Any())
            {
                var articles = GetArticlesList();
                foreach (var article in articles)
                    context.Articles.Add(article);
            }

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

        private static IEnumerable<Article> GetArticlesList()
        {
            return new[]
            {
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Coockies",
                    Description = "Netherlands coockies",
                    PriceExclVat = 1.98M
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Orange juice",
                    Description = "100% pure juice",
                    PriceExclVat = 0.99M
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Snickers",
                    Description = "Best chocolate bar in the world",
                    PriceExclVat = 0.49M
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Bananas",
                    Description = "High quality bananas from South Africa",
                    PriceExclVat = 0.99M
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Coca-cola",
                    Description = "A global leader in the beverage industry",
                    PriceExclVat = 0.39M
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Apples",
                    Description = "Apples from Poland. Most popular apples in the Europe",
                    PriceExclVat = 0.19M
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Vodka",
                    Description = "Cristal Vodka from Russia, once you try it you never stop drinking it",
                    PriceExclVat = 2.99M
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Salad",
                    Description = "Light salad (per kg)",
                    PriceExclVat = 0.59M
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Cucumbers",
                    Description = "Very green cucumbers (per kg)",
                    PriceExclVat = 0.29M
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Rice",
                    Description = "Black rice with vitamins (per kg)",
                    PriceExclVat = 0.79M
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Socks",
                    Description = "Men socks (per kg)",
                    PriceExclVat = 0.29M
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Toothpaste",
                    Description = "Coolgate whitening toothpaste, buy for lowest price ever",
                    PriceExclVat = 0.15M
                },
                new Article
                {
                    Id = Guid.NewGuid(),
                    Name = "Beef",
                    Description = "Fresh. Tough. Cheep. (per kg)",
                    PriceExclVat = 0.45M
                }
            }.AsQueryable();
        }
    }
}