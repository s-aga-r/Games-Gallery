using GamesGallery.BL;
using GamesGallery.PL.Web.Models;
using GamesGallery.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GamesGallery.PL.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
            GlobalFilters.Filters.Add(new GamesGalleryLogger());
            //Database.SetInitializer(new GamesGallerySeeder());
        }
    }
}
