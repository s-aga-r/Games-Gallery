using GamesGallery.BL.Repository;
using GamesGallery.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GamesGallery.PL.Web.Controllers
{
    public class PartialViewController : Controller
    {
        // Repository Instance
        private IGamesGalleryRepository _gamesGalleryRepository = null;


        // Public Constructor
        public PartialViewController(IGamesGalleryRepository gamesGalleryRepository)
        {
            _gamesGalleryRepository = gamesGalleryRepository;
        }


        [ChildActionOnly]
        public PartialViewResult _CategoryListForLayoutPartial()
        {
            return PartialView(_gamesGalleryRepository.GetActiveCategoriesAsList());
        }


        [ChildActionOnly]
        [OutputCache(Duration = 60 * 60)]
        public PartialViewResult _SliderPartial(List<SliderViewModel> Sliders)
        {
            return PartialView(Sliders);
        }


        [ChildActionOnly]
        public PartialViewResult _GamesPartial(IPagedList<GameViewModel> Games, string Title, string Parameter, string Action, string Controller, int? GamesCount)
        {
            ViewBag.Title = Title;
            ViewBag.Parameter = Parameter;
            ViewBag.Action = Action;
            ViewBag.Controller = Controller;
            ViewBag.GamesCount = GamesCount;
            return PartialView(Games);
        }
    }
}