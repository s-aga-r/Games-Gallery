using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Text;
using GamesGallery.BL.Repository;
using GamesGallery.ViewModel;
using PagedList;
using PagedList.Mvc;

namespace GamesGallery.PL.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        // Repository Instance
        private IGamesGalleryRepository _gamesGalleryRepository = null;


        // Public Constructor
        public GamesController(IGamesGalleryRepository gamesGalleryRepository)
        {
            _gamesGalleryRepository = gamesGalleryRepository;
        }


        // GET: Admin/Games
        public async Task<ActionResult> Index(string SearchBy, string Search, int? NoOfRecords, int? Page, string SortBy)
        {
            List<GameViewModel> Games = await _gamesGalleryRepository.GamesAsync();
            Games.AsQueryable();

            ViewBag.SortTitleParameter = SortBy == "Title" ? "Title desc" : "Title";
            ViewBag.SortSizeParameter = SortBy == "Size" ? "Size desc" : "Size";
            ViewBag.SortDateOfUploadParameter = string.IsNullOrEmpty(SortBy) ? "DateOfUpload desc" : "";
            ViewBag.SortYearOfReleaseParameter = SortBy == "YearOfRelease" ? "YearOfRelease desc" : "YearOfRelease";
            ViewBag.SortTotalDownloadsParameter = SortBy == "TotalDownloads" ? "TotalDownloads desc" : "TotalDownloads";

            if (!string.IsNullOrEmpty(Search))
            {
                Search = Search.ToLower();
                if (SearchBy == "Title")
                {
                    Games = Games.Where(x => x.Title.ToLower().StartsWith(Search)).ToList();
                }
                else
                {
                    Games = Games.Where(x => x.YearOfRelease.ToString().StartsWith(Search)).ToList();
                }
            }

            switch(SortBy)
            {
                case "DateOfUpload desc" :
                    Games = Games.OrderByDescending(x => x.DateOfUpload).ToList();
                    break;

                case "Size":
                    Games = Games.OrderBy(x => x.Size).ToList();
                    break;

                case "Size desc":
                    Games = Games.OrderByDescending(x => x.Size).ToList();
                    break;

                case "Title":
                    Games = Games.OrderBy(x => x.Title).ToList();
                    break;

                case "Title desc":
                    Games = Games.OrderByDescending(x => x.Title).ToList();
                    break;

                case "YearOfRelease":
                    Games = Games.OrderBy(x => x.YearOfRelease).ToList();
                    break;

                case "YearOfRelease desc":
                    Games = Games.OrderByDescending(x => x.YearOfRelease).ToList();
                    break;

                case "TotalDownloads":
                    Games = Games.OrderBy(x => x.TotalDownloads).ToList();
                    break;

                case "TotalDownloads desc":
                    Games = Games.OrderByDescending(x => x.TotalDownloads).ToList();
                    break;

                default:
                    Games = Games.OrderBy(x => x.DateOfUpload).ToList();
                    break;
            }

            return View(Games.ToPagedList(Page ?? 1, NoOfRecords ?? 10));
        }


        // GET: Admin/Games/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameViewModel game = await _gamesGalleryRepository.GameIncludeAllAsync(id.Value);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }


        // GET: Admin/Games/Create
        public async Task<ActionResult> Create()
        {
            GameViewModel game = new GameViewModel();
            game.CategoriesList = await _gamesGalleryRepository.CategoriesAsListOfSelectListItemAsync();
            ViewBag.ListSize = game.CategoriesList.Count() > 8 ? game.CategoriesList.Count() / 3 : 5;
            return View(game);
        }


        // POST: Admin/Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,CoverImage,Size,MinimumRequirements,RecommendedRequirements,VideoTutorial,YearOfRelease,IsActive,DownloadLink,CategoriesId,CategoriesList")] GameViewModel game, HttpPostedFileBase[] Screenshot)
        {
            if (game.CoverImage == null)
                ModelState.AddModelError("CoverImage", "This field is required.");
            if (game.DownloadLink == null)
                ModelState.AddModelError("DownloadLink", "This field is required.");
            if (Screenshot.Count() > 10)
                ModelState.AddModelError("Screenshot", "Maximum 10 images are allowed.");
            if (game.CategoriesId == null)
                ModelState.AddModelError("CategoriesId", "This field is required.");
            if (ModelState.IsValid)
            {
                game.CoverImageHelper = Request.Files["CoverImage"];
                game.Path = ConfigurationManager.AppSettings["SaveImagesTo"];
                game.ServerPath = Server.MapPath("~/");
                game.Screenshot = Screenshot;
                await _gamesGalleryRepository.AddGameAsync(game);

                return RedirectToAction("Index");
            }
            game.CategoriesList = await _gamesGalleryRepository.CategoriesAsListOfSelectListItemAsync();
            return View(game);
        }


        // GET: Admin/Games/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameViewModel game = await _gamesGalleryRepository.GameIncludeAllAsync(id.Value);
            if (game == null)
            {
                return HttpNotFound();
            }
            game.CategoriesList = await _gamesGalleryRepository.CategoriesAsListOfSelectListItemAsync();
            ViewBag.ListSize = game.CategoriesList.Count() > 8 ? game.CategoriesList.Count() / 3 : 5;
            return View(game);
        }


        // POST: Admin/Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,CoverImage,Size,MinimumRequirements,RecommendedRequirements,VideoTutorial,YearOfRelease,IsActive,TotalDownloads,DownloadLink,CategoriesId,DownloadLinkInsertionMode,ScreenshotInsertionMode,CategoriesInsertionMode")] GameViewModel game, HttpPostedFileBase[] Screenshot)
        {
            if (Screenshot.Count() > 10)
                ModelState.AddModelError("Screenshot", "Maximum 10 images are allowed.");
            if (ModelState.IsValid)
            {
                game.CoverImageHelper = Request.Files["CoverImage"];
                game.Path = ConfigurationManager.AppSettings["SaveImagesTo"];
                game.ServerPath = Server.MapPath("~/");
                game.Screenshot = Screenshot;
                await _gamesGalleryRepository.EditGameAsync(game);

                return RedirectToAction("Index");
            }
            return View(game);
        }


        // GET: Admin/Games/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameViewModel game = await _gamesGalleryRepository.GameIncludeAllAsync(id.Value);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }


        // POST: Admin/Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _gamesGalleryRepository.RemoveGameAsync(id, Server.MapPath("~/"));
            return RedirectToAction("Index");
        }


        // POST: Admin/Games/AddCategory
        [HttpPost]
        public async Task<JsonResult> AddCategory(string category)
        {
            CategoryViewModel categoryToBeAdded = await _gamesGalleryRepository.AddCategoryByTitleAsync(category);
            if (categoryToBeAdded == null)
            {
                var data = new
                {
                    Id = 0,
                    Title = "",
                    Message = "Category already in the list or its active status is false."
                };

                var json = JsonConvert.SerializeObject(data);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = new
                {
                    Id = categoryToBeAdded.Id,
                    Title = categoryToBeAdded.Title,
                    Message = "New Category Added : " + category
                };

                var json = JsonConvert.SerializeObject(data);
                return Json(json, JsonRequestBehavior.AllowGet);
            }
        }


        //Post: Admin/Games/DeleteSelectedGames
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteSelectedGames(List<int> IdsToDelete)
        {
            if(IdsToDelete != null)
            {
                await _gamesGalleryRepository.RemoveSelectedGamesAsync(IdsToDelete, Server.MapPath("~/"));
            }
            return RedirectToAction("Index");
        }


        //Get: Admin/Games/IsCategoryAvailable
        public async Task<JsonResult> IsCategoryAvailable(string AddCategory)
        {
            List<CategoryViewModel> categories = await _gamesGalleryRepository.CategoriesAsync();
            return Json(!categories.Any(x => x.Title.ToLower() == AddCategory.ToLower()), JsonRequestBehavior.AllowGet);
        }
    }
}
