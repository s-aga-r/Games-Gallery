using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.IO;
using GamesGallery.BL.Repository;
using GamesGallery.ViewModel;
using System.Configuration;
using PagedList;

namespace GamesGallery.PL.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ScreenshotsController : Controller
    {
        // Repository Instance
        private IGamesGalleryRepository _gamesGalleryRepository = null;


        // Public Constructor
        public ScreenshotsController(IGamesGalleryRepository gamesGalleryRepository)
        {
            _gamesGalleryRepository = gamesGalleryRepository;
        }


        // GET: Admin/Screenshots
        public async Task<ActionResult> Index(string Search, int? NoOfRecords, int? Page)
        {
            List<GameViewModel> Games = await _gamesGalleryRepository.GamesAsync();
            Games.AsQueryable();

            if (!string.IsNullOrEmpty(Search))
            {
                Search = Search.ToLower();
                Games = Games.Where(x => x.Title.ToLower().StartsWith(Search)).ToList();
            }

            return View(Games.ToPagedList(Page ?? 1, NoOfRecords ?? 12));
        }


        // GET: Admin/Screenshots/Create
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameViewModel game = await _gamesGalleryRepository.GameAsync(id.Value);
            if (game == null)
            {
                return HttpNotFound();
            }
            ScreenshotViewModel screenshot = new ScreenshotViewModel()
            {
                Id = 0,
                Title = game.Title
            };
            ViewBag.GameId = id;
            return View(screenshot);
        }


        // POST: Admin/Screenshots/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,ImagePath,IsActive")] ScreenshotViewModel screenshot, int id)
        {
            if (screenshot.ImagePath == null)
                ModelState.AddModelError("ImagePath", "This field is required.");
            if (ModelState.IsValid)
            {
                screenshot.GameId = id;
                screenshot.ImageHelper = Request.Files["ImagePath"];
                screenshot.Path = ConfigurationManager.AppSettings["SaveImagesTo"];
                screenshot.ServerPath = Server.MapPath("~/");
                if (await _gamesGalleryRepository.AddScreenshotAsync(screenshot))
                {
                    return RedirectToAction("Game", new { id = id });
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(screenshot);
        }


        // GET: Admin/Screenshots/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScreenshotViewModel screenshot = await _gamesGalleryRepository.ScreenshotDetailsIncludeAllAsync(id.Value);
            if (screenshot == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameId = screenshot.Game.Id;
            return View(screenshot);
        }


        // POST: Admin/Screenshots/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,ImagePath,IsActive,Game")] ScreenshotViewModel screenshot)
        {
            if (ModelState.IsValid)
            {
                screenshot.ImageHelper = Request.Files["ImagePath"];
                screenshot.Path = ConfigurationManager.AppSettings["SaveImagesTo"];
                screenshot.ServerPath = Server.MapPath("~/");
                await _gamesGalleryRepository.EditScreenshotAsync(screenshot);

                //return RedirectToAction("Game", new { id = screenshot.Game.Id });
                return RedirectToAction("Index");
            }
            return View(screenshot);
        }


        // GET: Admin/Screenshots/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScreenshotViewModel screenshot = await _gamesGalleryRepository.ScreenshotDetailsIncludeAllAsync(id.Value);
            if (screenshot == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameId = screenshot.Game.Id;
            return View(screenshot);
        }


        // POST: Admin/Screenshots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _gamesGalleryRepository.RemoveScreenshotAsync(id, Server.MapPath("~/"));
            return RedirectToAction("Index");
        }


        // GET: /Admin/Screenshots/Game/1
        public async Task<ActionResult> Game(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<ScreenshotViewModel> screenshots = await _gamesGalleryRepository.ScreenshotsListByGameIdAsync(id.Value);
            //if (screenshots.Count() < 1)
            //{
            //    return HttpNotFound();
            //}
            ViewBag.GameId = id;
            return View(screenshots);
        }


        //Post: Admin/Screenshots/DeleteSelectedScreenshots
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteSelectedScreenshots(List<int> IdsToDelete)
        {
            if (IdsToDelete != null)
            {
                await _gamesGalleryRepository.RemoveSelectedScreenshotAsync(IdsToDelete, Server.MapPath("~/"));
            }
            return RedirectToAction("Index");
        }
    }
}
