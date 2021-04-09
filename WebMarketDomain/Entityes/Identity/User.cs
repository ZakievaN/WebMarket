using Microsoft.AspNetCore.Identity;

namespace WebMarketDomain.Entityes.Identity
{
    public class User : IdentityUser
    {
        public const string Administrator = "Administrator";

        public const string DefaultAdminPassword = "hjbYt7878YGJH";
    }
}
