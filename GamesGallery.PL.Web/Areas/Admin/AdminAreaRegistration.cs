using System.Web.Mvc;

namespace GamesGallery.PL.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controler = "Home" , action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "GamesGallery.PL.Web.Areas.Admin.Controllers" }
            );
        }
    }
}