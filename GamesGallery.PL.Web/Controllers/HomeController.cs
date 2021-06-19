using GamesGallery.BL.Repository;
using GamesGallery.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GamesGallery.PL.Web.Controllers
{
    public class HomeController : Controller
    {
        // Repository Instance
        private IGamesGalleryRepository _gamesGalleryRepository = null;


        // Public Constructor
        public HomeController(IGamesGalleryRepository gamesGalleryRepository)
        {
            _gamesGalleryRepository = gamesGalleryRepository;
        }


        // Get: Home/Index
        public async Task<ActionResult> Index(int? Page, int? NoOfGames)
        {
            List<GameViewModel> Games = await _gamesGalleryRepository.GetActiveGamesIncludeAllAsync();
            IPagedList<GameViewModel> GamesAsPagedList = Games.ToPagedList(Page ?? 1, NoOfGames ?? 10);

            IndexViewModel model = new IndexViewModel();
            model.Games = GamesAsPagedList;
            model.Sliders = await _gamesGalleryRepository.GetActiveSlidersIncludeAllAsync();

            return View(model);
        }


        // Get: Home/About
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        // Get: Home/Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}