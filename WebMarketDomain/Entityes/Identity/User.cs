using Microsoft.AspNetCore.Identity;

namespace WebMarketDomain.Entityes.Identity
{
    public class User : IdentityUser
    {
        public const string Administrator = "Admin";

        public const string DefaultAdminPassword = "123456";
    }
}
