using Microsoft.AspNetCore.Identity;

namespace WebMarketDomain.Entityes.Identity
{
    public class User : IdentityUser
    {
        public const string Administartor = "Admin";

        public const string DefaultAdminPassword = "hjbYt7878YGJH";
    }
}
