using Microsoft.AspNetCore.Mvc;

namespace WebMarket.Components
{
    public class UserInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return User.Identity?.IsAuthenticated == true ? View("UserInfo") : View("Default");
        }
    }
}
