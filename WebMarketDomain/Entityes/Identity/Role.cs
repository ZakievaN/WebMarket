using Microsoft.AspNetCore.Identity;

namespace WebMarketDomain.Entityes.Identity
{
    public class Role : IdentityRole
    {
        public const string Administartor = "Administrator";

        public const string Users = "User";
    }
}
