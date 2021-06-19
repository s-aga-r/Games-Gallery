using GamesGallery.BL.Repository;
using GamesGallery.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GamesGallery.PL.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class SliderController : Controller
    {
        // Repository Instance
        private IGamesGalleryRepository _gamesGalleryRepository = null;


        // Public Constructor
        public SliderController(IGamesGalleryRepository gamesGalleryRepository)
        {
            _gamesGalleryRepository = gamesGalleryRepository;
        }


        // GET: Admin/Slider
        public async Task<ActionResult> Index(string Search, int? NoOfRecords, int? Page, string SortBy)
        {
            List<SliderViewModel> sliders = await _gamesGalleryRepository.SlidersIncludeAllAsync();
            sliders.AsQueryable();

            ViewBag.SortOrderParameter = string.IsNullOrEmpty(SortBy) ? "Order desc" : "";
            ViewBag.SortGameTitleParameter = SortBy == "GameTitle" ? "GameTitle desc" : "GameTitle";
            ViewBag.SortDateOfUploadParameter = SortBy == "DateOfUpload" ? "DateOfUpload desc" : "DateOfUpload";
            ViewBag.SortLastUpdatedOnParameter = SortBy == "LastUpdatedOn" ? "LastUpdatedOn desc" : "LastUpdatedOn";

            if (!string.IsNullOrEmpty(Search))
            {
                Search = Search.ToLower();
                sliders = sliders.Where(x => x.Game.Title.ToLower().StartsWith(Search)).ToList();
            }

            switch (SortBy)
            {
                case "GameTitle":
                    sliders = sliders.OrderBy(x => x.Game.Title).ToList();
                    break;

                case "GameTitle desc":
                    sliders = sliders.OrderByDescending(x => x.Game.Title).ToList();
                    break;

                case "DateOfUpload":
                    sliders = sliders.OrderBy(x => x.DateOfUpload).ToList();
                    break;

                case "DateOfUpload desc":
                    sliders = sliders.OrderByDescending(x => x.DateOfUpload).ToList();
                    break;

                case "LastUpdatedOn":
                    sliders = sliders.OrderBy(x => x.LastUpdatedOn).ToList();
                    break;

                case "LastUpdatedOn desc":
                    sliders = sliders.OrderByDescending(x => x.LastUpdatedOn).ToList();
                    break;

                case "Order desc":
                    sliders = sliders.OrderByDescending(x => x.Order).ToList();
                    break;

                default:
                    sliders = sliders.OrderBy(x => x.Order).ToList();
                    break;
            }

            return View(sliders.ToPagedList(Page ?? 1, NoOfRecords ?? 10));
        }


        // GET: Admin/Slider/Create
        public async Task<ActionResult> Create()
        {
            SliderViewModel slider = new SliderViewModel();
            slider.GamesList = await _gamesGalleryRepository.GamesAsListOfSelectListItemAsync();
            return View(slider);
        }


        // POST: Admin/Slider/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SlideImage,GameId,GamesList,Order,IsActive")] SliderViewModel slider)
        {
            if (slider.SlideImage == null)
                ModelState.AddModelError("SlideImage", "This field is required.");
            if (slider.GameId == 0)
                ModelState.AddModelError("GameId", "This field is required.");
            if (ModelState.IsValid)
            {
                slider.SlideImageHelper = Request.Files["SlideImage"];
                slider.Path = ConfigurationManager.AppSettings["SaveImagesTo"];
                slider.ServerPath = Server.MapPath("~/");
                await _gamesGalleryRepository.AddSliderAsync(slider);

                return RedirectToAction("Index");
            }
            //slider.GamesList = await _gamesGalleryRepository.GamesAsListOfSelectListItemAsync();
            return View(slider);
        }


        // GET: Admin/Slider/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderViewModel slider = await _gamesGalleryRepository.SliderIncludeAllAsync(id.Value);
            if (slider == null)
            {
                return HttpNotFound();
            }
            slider.GamesList = await _gamesGalleryRepository.GamesAsListOfSelectListItemAsync();
            return View(slider);
        }


        // POST: Admin/Slider/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SlideImage,GameId,GamesList,Order,IsActive")] SliderViewModel slider)
        {
            if (ModelState.IsValid)
            {
                slider.SlideImageHelper = Request.Files["SlideImage"];
                slider.Path = ConfigurationManager.AppSettings["SaveImagesTo"];
                slider.ServerPath = Server.MapPath("~/");
                await _gamesGalleryRepository.EditSliderAsync(slider);

                return RedirectToAction("Index");
            }
            return View(slider);
        }


        // GET: Admin/Slider/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderViewModel slider = await _gamesGalleryRepository.SliderIncludeAllAsync(id.Value);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }


        // POST: Admin/Slider/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _gamesGalleryRepository.RemoveSliderAsync(id, Server.MapPath("~/"));
            return RedirectToAction("Index");
        }


        //Post: Admin/Slider/DeleteSelectedSliders
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteSelectedSliders(List<int> IdsToDelete)
        {
            if (IdsToDelete != null)
            {
                await _gamesGalleryRepository.RemoveSelectedSlidersAsync(IdsToDelete, Server.MapPath("~/"));
            }
            return RedirectToAction("Index");
        }
    }
}
