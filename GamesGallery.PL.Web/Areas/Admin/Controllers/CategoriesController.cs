using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamesGallery.BL.Repository;
using GamesGallery.ViewModel;
using PagedList;
using GamesGallery.PL.Web.Models;

namespace GamesGallery.PL.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        // Repository Instance
        private IGamesGalleryRepository _gamesGalleryRepository = null;


        // Public Constructor
        public CategoriesController(IGamesGalleryRepository gamesGalleryRepository)
        {
            _gamesGalleryRepository = gamesGalleryRepository;
        }


        // GET: Admin/Categories
        public async Task<ActionResult> Index(string Search, int? NoOfRecords, int? Page, string SortBy)
        {
            List<CategoryViewModel> Categories = await _gamesGalleryRepository.CategoriesAsync();
            Categories.AsQueryable();

            ViewBag.SortTitleParameter = string.IsNullOrEmpty(SortBy) ? "Title desc" : "";

            if (!string.IsNullOrEmpty(Search))
            {
                Search = Search.ToLower();
                Categories = Categories.Where(x => x.Title.ToLower().StartsWith(Search)).ToList();
            }

            switch (SortBy)
            {
                case "Title desc":
                    Categories = Categories.OrderByDescending(x => x.Title).ToList();
                    break;
                default:
                    Categories = Categories.OrderBy(x => x.Title).ToList();
                    break;
            }

            return View(Categories.ToPagedList(Page ?? 1, NoOfRecords ?? 10));
        }


        // GET: Admin/Categories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryViewModel category = await _gamesGalleryRepository.CategoryIncludeAllAsync(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            return View(new CategoryViewModel());
        }


        // POST: Admin/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,IsActive")] CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                await _gamesGalleryRepository.AddCategoryAsync(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }


        // GET: Admin/Categories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryViewModel category = await _gamesGalleryRepository.CategoryIncludeAllAsync(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        // POST: Admin/Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,IsActive")] CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                await _gamesGalleryRepository.EditCategoryAsync(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }


        // GET: Admin/Categories/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryViewModel category = await _gamesGalleryRepository.CategoryIncludeAllAsync(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _gamesGalleryRepository.RemoveCategoryAsync(id);
            return RedirectToAction("Index");
        }


        //Post: Admin/Categories/DeleteSelectedCategories
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteSelectedCategories(List<int> IdsToDelete)
        {
            if (IdsToDelete != null)
            {
                await _gamesGalleryRepository.RemoveSelectedCategoryAsync(IdsToDelete);
            }
            return RedirectToAction("Index");
        }
    }
}
