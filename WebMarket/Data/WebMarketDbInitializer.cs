using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using WebMarket.DAL.Context;

namespace WebMarket.Data
{
    public class WebMarketDbInitializer
    {
        private readonly WebMarketDB _db;

        private ILogger<WebMarketDbInitializer> _logger;

        public WebMarketDbInitializer(WebMarketDB db, ILogger<WebMarketDbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void Initialize()
        {
            _logger.LogInformation("Инициализация БД");

            if (_db.Database.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Выполнение миграции БД");
                _db.Database.Migrate();
                _logger.LogInformation("Выполнение миграции БД выполнена успешно");
            }

            try
            {
                InitializeProduct();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ошибка инициализации товаров");
                throw;
            }

            _logger.LogInformation("Инициализация БД выполнена успешно");
        }

        private void InitializeProduct()
        {
            if (_db.Products.Any())
            {
                _logger.LogInformation("Инициализация товаров не нужна");
                return;
            }

            _logger.LogInformation("Инициализация секций");
            using (_db.Database.BeginTransaction())
            {
                _db.Sections.AddRange(TestData.Sections);

                _db.Database.ExecuteSqlRaw("set identity_insert [dbo].[Sections] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("set identity_insert [dbo].[Sections] OFF");
                
                _db.Database.CommitTransaction();
            }

            _logger.LogInformation("Инициализация брендов");
            using (_db.Database.BeginTransaction())
            {
                _db.Brands.AddRange(TestData.Brands);

                _db.Database.ExecuteSqlRaw("set identity_insert [dbo].[Brands] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("set identity_insert [dbo].[Brands] OFF");

                _db.Database.CommitTransaction();
            }

            _logger.LogInformation("Инициализация товаров");
            using (_db.Database.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);

                _db.Database.ExecuteSqlRaw("set identity_insert [dbo].[Products] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("set identity_insert [dbo].[Products] OFF");

                _db.Database.CommitTransaction();
            }

            _logger.LogInformation("Инициализация товаров завершена.");
        }
    }
}
