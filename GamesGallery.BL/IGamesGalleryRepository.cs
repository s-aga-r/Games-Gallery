using GamesGallery.DL;
using GamesGallery.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GamesGallery.BL.Repository
{
    public interface IGamesGalleryRepository
    {
        Task<bool> AddCategoryAsync(CategoryViewModel model);
        Task<CategoryViewModel> AddCategoryByTitleAsync(string title);
        Task<bool> AddGameAsync(GameViewModel model);
        Task<bool> AddScreenshotAsync(ScreenshotViewModel model);
        Task<bool> AddSliderAsync(SliderViewModel model);
        Task<List<SelectListItem>> CategoriesAsListOfSelectListItemAsync();
        Task<List<CategoryViewModel>> CategoriesAsync();
        Task<List<CategoryViewModel>> CategoriesIncludeAllAsync();
        Task<CategoryViewModel> CategoryIncludeAllAsync(int id);
        Task<bool> EditCategoryAsync(CategoryViewModel model);
        Task<bool> EditGameAsync(GameViewModel model);
        Task<bool> EditScreenshotAsync(ScreenshotViewModel model);
        Task<bool> EditSliderAsync(SliderViewModel model);
        Task<GameViewModel> GameAsync(int id);
        Task<GameViewModel> GameIncludeAllAsync(int id);
        Task<List<SelectListItem>> GamesAsListOfSelectListItemAsync();
        Task<List<GameViewModel>> GamesAsync();
        Task<bool> GameDownloadedAsync(int id);
        Task<List<GameViewModel>> GamesIncludeAllAsync();
        List<string> GetActiveCategoriesAsList();
        Task<List<GameViewModel>> GetActiveGamesAsync();
        Task<List<GameViewModel>> GetActiveGamesByCategoryAsync(string category);
        Task<List<GameViewModel>> GetActiveGamesByCategoryIncludeAllAsync(string category);
        Task<List<GameViewModel>> GetActiveGamesByTitleAsync(string title);
        Task<List<GameViewModel>> GetActiveGamesByTitleIncludeAllAsync(string title);
        Task<List<GameViewModel>> GetActiveGamesIncludeAllAsync();
        Task<List<SliderViewModel>> GetActiveSlidersAsync();
        Task<List<SliderViewModel>> GetActiveSlidersIncludeAllAsync();
        Task<bool> RemoveAllDownloadLinksAsync(List<DownloadLink> downloadLinks);
        Task<bool> RemoveAllScreenshotsAsync(List<Screenshot> screenshots, string serverPath);
        Task<bool> RemoveCategoryAsync(int id);
        Task<bool> RemoveDownloadLinkAsync(int id);
        Task<bool> RemoveGameAsync(int id, string serverPath);
        Task<bool> RemoveScreenshotAsync(int id, string serverPath);
        Task<bool> RemoveSelectedCategoryAsync(List<int> CategoriesId);
        Task<bool> RemoveSelectedGamesAsync(List<int> GamesId, string serverPath);
        Task<bool> RemoveSelectedScreenshotAsync(List<int> ScreenshotsId, string serverPath);
        Task<bool> RemoveSelectedSlidersAsync(List<int> slidersId, string serverPath);
        Task<bool> RemoveSliderAsync(int id, string serverPath);
        Task<ScreenshotViewModel> ScreenshotDetailsIncludeAllAsync(int id);
        Task<List<ScreenshotViewModel>> ScreenshotsListByGameIdAsync(int gameId);
        Task<SliderViewModel> SliderAsync(int id);
        Task<SliderViewModel> SliderIncludeAllAsync(int id);
        Task<List<SliderViewModel>> SlidersAsync();
        Task<List<SliderViewModel>> SlidersIncludeAllAsync();
        Task<List<Slider>> SlidersListByGameIdAsync(int GameId);
        Task<List<Slider>> SlidersListByGameIdIncludeAllAsync(int GameId);
    }
}