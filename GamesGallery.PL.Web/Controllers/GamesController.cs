using GamesGallery.BL.Repository;
using GamesGallery.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GamesGallery.PL.Web.Controllers
{
    public class GamesController : Controller
    {
        // Repository Instance
        private IGamesGalleryRepository _gamesGalleryRepository = null;


        // Public Constructor
        public GamesController(IGamesGalleryRepository gamesGalleryRepository)
        {
            _gamesGalleryRepository = gamesGalleryRepository;
        }


        // Get: Games/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameViewModel game = await _gamesGalleryRepository.GameIncludeAllAsync(id.Value);
            if (game == null || game.IsActive == false || game.Categories.Any(y => y.IsActive == false))
            {
                return HttpNotFound();
            }
            game.Screenshots = game.Screenshots.Where(x => x.IsActive == true).ToList();
            game.DownloadLinks = game.DownloadLinks.Where(x => x.IsActive == true).ToList();

            return View(game);
        }


        // Get: Games/Category/Adventure
        [Route("~/Games/Category/{category:minlength(3):maxlength(25)}")]
        public async Task<ActionResult> Category(string category, int? Page, int? NoOfGames)
        {
            if (string.IsNullOrEmpty(category))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<GameViewModel> games = await _gamesGalleryRepository.GetActiveGamesByCategoryIncludeAllAsync(category);
            if (games == null)
            {
                return HttpNotFound();
            }

            ViewBag.Parameter = category;
            ViewBag.GamesCount = games.Count();

            return View(games.ToPagedList(Page ?? 1, NoOfGames ?? 10));
        }


        // Only for Redirection
        // Get: Games/SearchGame/Pubg
        public ActionResult SearchGame(string SearchString)
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Search", new { SearchString = SearchString });
        }


        // Get: Games/Search/Pubg
        [Route("~/Games/Search/{SearchString:minlength(1):maxlength(25)}")]
        public async Task<ActionResult> Search(string SearchString, int? Page, int? NoOfGames)
        {
            List<GameViewModel> games = await _gamesGalleryRepository.GetActiveGamesByTitleIncludeAllAsync(SearchString);

            ViewBag.Parameter = SearchString;
            ViewBag.GamesCount = games.Count();

            return View(games.ToPagedList(Page ?? 1, NoOfGames ?? 10));
        }


        // Post: Games/TotalDownloadsIncrement/5
        [HttpPost]
        public async Task<JsonResult> TotalDownloadsIncrement(int id)
        {
            bool result = await _gamesGalleryRepository.GameDownloadedAsync(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // Get: Games/TopGames
        public async Task<ActionResult> TopGames(int? Page, int? NoOfGames)
        {
            List<GameViewModel> Games = await _gamesGalleryRepository.GetActiveGamesIncludeAllAsync();
            Games.OrderByDescending(x => x.TotalDownloads);
            IPagedList<GameViewModel> GamesAsPagedList = Games.ToPagedList(Page ?? 1, NoOfGames ?? 10);

            return View(GamesAsPagedList);
        }
    }
}