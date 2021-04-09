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
        private readonly UserManager<User> _userManger;
        private readonly RoleManager<Role> _roleManager;
        private ILogger<WebMarketDbInitializer> _logger;

        public WebMarketDbInitializer(
            WebMarketDB db, 
            UserManager<User> userManger,
            RoleManager<Role> roleManager,
            ILogger<WebMarketDbInitializer> logger
            )
        {
            _db = db;
            _userManger = userManger;
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

            async Task ChechRole(string RoleName)
            {
                if (await _roleManager.RoleExistsAsync(RoleName))
                {
                    _logger.LogInformation($"Роль {RoleName} отсутствует, создается..");
                    
                    await _roleManager.CreateAsync(new Role { Name = RoleName });
                    
                    _logger.LogInformation($"Роль {RoleName} создана успешно");
                }
            }

            await ChechRole(Role.Administartor);
            await ChechRole(Role.Users);

            if (await _userManger.FindByNameAsync(User.Administartor) is null)
            {
                _logger.LogInformation($"Учетная запись администратора отсутствует, создается..");

                var admin = new User
                {
                    UserName = User.Administartor
                };

                var creation_result = await _userManger.CreateAsync(admin, User.DefaultAdminPassword);
                if (creation_result.Succeeded)
                {
                    _logger.LogInformation($"Учетная запись администратора создана успешно");
                    
                    await _userManger.AddToRoleAsync(admin, Role.Administartor);
                    
                    _logger.LogInformation($"К учетной записи администратора добавлена роль администратора");
                }
                else
                {
                    var errors = creation_result.Errors.Select(e => e.Description).ToArray();

                    _logger.LogInformation($"Учетная запись администратора создана с ошибкой {string.Join(", ", errors)}");

                    throw new InvalidOperationException($"Ошибка при создании учетной записи администратора: {string.Join(", ", errors)}");
                }
            }
            _logger.LogInformation("Инициализация БД системы Identity выполнена успешно");
        }
    }
}
