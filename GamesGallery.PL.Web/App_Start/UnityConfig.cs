using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using GamesGallery.BL.Repository;
using GamesGallery.PL.Web.Controllers;
using Unity.Injection;

namespace GamesGallery.PL.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IGamesGalleryRepository, GamesGalleryRepository>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}