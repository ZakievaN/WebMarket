using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.DAL.Context;
using WebMarketDomain.Entityes.Identity;

namespace WebMarket.Data
{
    public class WebMarketDbInitializer
    {
        private readonly WebMarketDB _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private ILogger<WebMarketDbInitializer> _logger;

        public WebMarketDbInitializer(
            WebMarketDB db, 
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ILogger<WebMarketDbInitializer> logger
            )
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
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

            try
            {
                InitializeIdentityAsync().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ошибка инициализации данным системы Identity");
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

        private async Task InitializeIdentityAsync()
        {
            _logger.LogInformation("Инициализация БД системы Identity");

            async Task CheckRole(string RoleName)
            {
                if (!await _roleManager.RoleExistsAsync(RoleName))
                {
                    _logger.LogInformation("Роль {0} отсутствует. Создаю...", RoleName);
                    await _roleManager.CreateAsync(new Role { Name = RoleName });
                    _logger.LogInformation("Роль {0} создана успешно", RoleName);
                }
            }

            await CheckRole(Role.Administrator);
            await CheckRole(Role.Users);

            if (await _userManager.FindByNameAsync(User.Administrator) is null)
            {
                _logger.LogInformation("Учётная запись администратора в БД отсутствует. Создаю...");

                var admin = new User
                {
                    UserName = User.Administrator
                };

                var creation_result = await _userManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (creation_result.Succeeded)
                {
                    _logger.LogInformation("Учётная запись администратора создана успешно.");

                    await _userManager.AddToRoleAsync(admin, Role.Administrator);

                    _logger.LogInformation("Учётная запись администратора  наделена ролью администратора.");
                }
                else
                {
                    var errors = creation_result.Errors.Select(e => e.Description).ToArray();
                    _logger.LogInformation("Учётная запись администратора создана с ошибкой {0}", string.Join(",", errors));

                    throw new InvalidOperationException($"Ошибка при создании учётной записи администратора: {string.Join(",", errors)}");
                }
            }

            _logger.LogInformation("Инициализация БД системы Identity выполнена");
        }
    }
}
