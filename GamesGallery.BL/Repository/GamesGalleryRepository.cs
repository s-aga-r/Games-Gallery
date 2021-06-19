using GamesGallery.DL;
using GamesGallery.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;

namespace GamesGallery.BL.Repository
{
    // Contains Private Helper Methods
    public partial class GamesGalleryRepository : IGamesGalleryRepository
    {
        private GamesGalleryDbContext context = new GamesGalleryDbContext();

        #region Game

        private async Task<Game> GetGameAsync(int id)
        {
            return await context.Games.FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<Game> GetGameIncludeAllAsync(int id)
        {
            return await context.Games.Include(x => x.DownloadLinks).Include(x => x.Screenshots).Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<List<Game>> GetGamesAsync()
        {
            return await context.Games.ToListAsync();
        }

        private async Task<List<Game>> GetGamesIncludeAllAsync()
        {
            return await context.Games.Include(x => x.TotalDownloads).Include(x => x.Screenshots).Include(x => x.Categories).ToListAsync();
        }

        private GameViewModel GameToGameViewModel(Game game)
        {
            GameViewModel model = new GameViewModel()
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                CoverImage = game.CoverImage,
                Size = game.Size,
                TotalDownloads = game.TotalDownloads,
                MinimumRequirements = game.MinimumRequirements,
                RecommendedRequirements = game.RecommendedRequirements,
                VideoTutorial = game.VideoTutorial,
                DateOfUpload = game.DateOfUpload,
                YearOfRelease = game.YearOfRelease,
                DownloadLinks = null,
                Screenshots = null,
                Categories = null,
                LastUpdatedOn = game.LastUpdatedOn,
                IsActive = game.IsActive
            };

            return model;
        }

        private List<GameViewModel> GamesToGameViewModels(List<Game> games)
        {
            List<GameViewModel> model = new List<GameViewModel>();
            foreach (Game game in games)
            {
                model.Add(GameToGameViewModel(game));
            }

            return model;
        }

        private GameViewModel GameToGameViewModelIncludeAll(Game game)
        {
            GameViewModel model = new GameViewModel()
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                CoverImage = game.CoverImage,
                Size = game.Size,
                TotalDownloads = game.TotalDownloads,
                MinimumRequirements = game.MinimumRequirements,
                RecommendedRequirements = game.RecommendedRequirements,
                VideoTutorial = game.VideoTutorial,
                DateOfUpload = game.DateOfUpload,
                YearOfRelease = game.YearOfRelease,
                DownloadLinks = DownloadLinksToDownloadLinkViewModels(game.DownloadLinks.ToList()),
                Screenshots = ScreenshotsToScreenshotViewModels(game.Screenshots.ToList()),
                Categories = CategoriesToCategoryViewModels(game.Categories.ToList()),
                LastUpdatedOn = game.LastUpdatedOn,
                IsActive = game.IsActive
            };

            return model;
        }

        private List<GameViewModel> GamesToGameViewModelsIncludeAll(List<Game> games)
        {
            List<GameViewModel> model = new List<GameViewModel>();
            foreach (Game game in games)
            {
                model.Add(GameToGameViewModelIncludeAll(game));
            }

            return model;
        }

        private Game GameViewModelToGame(GameViewModel game)
        {
            Game model = new Game()
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                CoverImage = game.CoverImage,
                Size = game.Size,
                TotalDownloads = game.TotalDownloads,
                MinimumRequirements = game.MinimumRequirements,
                RecommendedRequirements = game.RecommendedRequirements,
                VideoTutorial = game.VideoTutorial,
                DateOfUpload = game.DateOfUpload,
                YearOfRelease = game.YearOfRelease,
                DownloadLinks = null,
                Screenshots = null,
                Categories = null,
                LastUpdatedOn = game.LastUpdatedOn,
                IsActive = game.IsActive
            };

            return model;
        }

        private List<Game> GameViewModelsToGames(List<GameViewModel> games)
        {
            List<Game> model = new List<Game>();
            foreach (GameViewModel game in games)
            {
                model.Add(GameViewModelToGame(game));
            }

            return model;
        }

        private Game GameViewModelToGameIncludeAll(GameViewModel game)
        {
            Game model = new Game()
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                CoverImage = game.CoverImage,
                Size = game.Size,
                TotalDownloads = game.TotalDownloads,
                MinimumRequirements = game.MinimumRequirements,
                RecommendedRequirements = game.RecommendedRequirements,
                VideoTutorial = game.VideoTutorial,
                DateOfUpload = game.DateOfUpload,
                YearOfRelease = game.YearOfRelease,
                DownloadLinks = DownloadLinkViewModelsToDownloadLinks(game.DownloadLinks.ToList()),
                Screenshots = ScreenshotViewModelsToScreenshots(game.Screenshots.ToList()),
                Categories = CategoryViewModelsToCategories(game.Categories.ToList()),
                LastUpdatedOn = game.LastUpdatedOn,
                IsActive = game.IsActive
            };

            return model;
        }

        private List<Game> GameViewModelsToGamesIncludeAll(List<GameViewModel> games)
        {
            List<Game> model = new List<Game>();
            foreach (GameViewModel game in games)
            {
                model.Add(GameViewModelToGameIncludeAll(game));
            }

            return model;
        }

        #endregion Game

        #region Category

        private async Task<Category> GetCategoryAsync(int id)
        {
            return await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<Category> GetCategoryIncludeAllAsync(int id)
        {
            return await context.Categories.Include(x => x.Games).FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<List<Category>> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        private async Task<List<Category>> GetCategoriesIncludeAllAsync()
        {
            return await context.Categories.Include(x => x.Games).ToListAsync();
        }

        private async Task<List<Category>> GetCategoriesById(List<int> categoriesId)
        {
            return await context.Categories.Where(x => categoriesId.Contains(x.Id)).ToListAsync();
        }

        private async Task<bool> GetCategoryStatusByTitle(string title)
        {
            if (await context.Categories.AnyAsync(x => x.Title == title))
            {
                return true;
            }

            return false;
        }

        private CategoryViewModel CategoryToCategoryViewModel(Category category)
        {
            CategoryViewModel model = new CategoryViewModel()
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description,
                Games = null,
                IsActive = category.IsActive
            };

            return model;
        }

        private List<CategoryViewModel> CategoriesToCategoryViewModels(List<Category> categories)
        {
            List<CategoryViewModel> model = new List<CategoryViewModel>();
            foreach (Category category in categories)
            {
                model.Add(CategoryToCategoryViewModel(category));
            }

            return model;
        }

        private CategoryViewModel CategoryToCategoryViewModelIncludeAll(Category category)
        {
            CategoryViewModel model = new CategoryViewModel()
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description,
                Games = GamesToGameViewModels(category.Games.ToList()),
                IsActive = category.IsActive
            };

            return model;
        }

        private List<CategoryViewModel> CategoriesToCategoryViewModelsIncludeAll(List<Category> categories)
        {
            List<CategoryViewModel> model = new List<CategoryViewModel>();
            foreach (Category category in categories)
            {
                model.Add(CategoryToCategoryViewModelIncludeAll(category));
            }

            return model;
        }

        private Category CategoryViewModelToCategory(CategoryViewModel category)
        {
            Category model = new Category()
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description,
                Games = null,
                IsActive = category.IsActive
            };

            return model;
        }

        private List<Category> CategoryViewModelsToCategories(List<CategoryViewModel> categories)
        {
            List<Category> model = new List<Category>();
            foreach (CategoryViewModel category in categories)
            {
                model.Add(CategoryViewModelToCategory(category));
            }

            return model;
        }

        private Category CategoryViewModelToCategoryIncludeAll(CategoryViewModel category)
        {
            Category model = new Category()
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description,
                Games = GameViewModelsToGames(category.Games.ToList()),
                IsActive = category.IsActive
            };

            return model;
        }

        private List<Category> CategoryViewModelsToCategoriesIncludeAll(List<CategoryViewModel> categories)
        {
            List<Category> model = new List<Category>();
            foreach (CategoryViewModel category in categories)
            {
                model.Add(CategoryViewModelToCategoryIncludeAll(category));
            }

            return model;
        }

        #endregion Category

        #region Screenshot

        private async Task<Screenshot> GetScreenshotAsync(int id)
        {
            return await context.Screenshots.FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<Screenshot> GetScreenshotIncludeAllAsync(int id)
        {
            return await context.Screenshots.Include(x => x.Game).FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<List<Screenshot>> GetScreenshotsAsync()
        {
            return await context.Screenshots.ToListAsync();
        }

        private async Task<List<Screenshot>> GetScreenshotsIncludeAllAsync()
        {
            return await context.Screenshots.Include(x => x.Game).ToListAsync();
        }

        private async Task<List<Screenshot>> GetScreenshotsByGameIdAsync(int gameId)
        {
            return await context.Screenshots.Where(x => x.GameId == gameId).ToListAsync();
        }

        private Screenshot ScreenshotViewModelToScreenshot(ScreenshotViewModel screenshot)
        {
            Screenshot model = new Screenshot()
            {
                Id = screenshot.Id,
                Title = screenshot.Title,
                Description = screenshot.Description,
                ImagePath = screenshot.ImagePath,
                GameId = screenshot.GameId,
                Game = null,
                IsActive = screenshot.IsActive
            };

            return model;
        }

        private List<Screenshot> ScreenshotViewModelsToScreenshots(List<ScreenshotViewModel> Screenshots)
        {
            List<Screenshot> model = new List<Screenshot>();
            foreach (ScreenshotViewModel screenshot in Screenshots)
            {
                model.Add(ScreenshotViewModelToScreenshot(screenshot));
            }

            return model;
        }

        private Screenshot ScreenshotViewModelToScreenshotIncludeAll(ScreenshotViewModel screenshot)
        {
            Screenshot model = new Screenshot()
            {
                Id = screenshot.Id,
                Title = screenshot.Title,
                Description = screenshot.Description,
                ImagePath = screenshot.ImagePath,
                GameId = screenshot.GameId,
                Game = GameViewModelToGame(screenshot.Game),
                IsActive = screenshot.IsActive
            };

            return model;
        }

        private List<Screenshot> ScreenshotViewModelsToScreenshotsIncludeAll(List<ScreenshotViewModel> Screenshots)
        {
            List<Screenshot> model = new List<Screenshot>();
            foreach (ScreenshotViewModel screenshot in Screenshots)
            {
                model.Add(ScreenshotViewModelToScreenshotIncludeAll(screenshot));
            }

            return model;
        }

        private ScreenshotViewModel ScreenshotToScreenshotViewModel(Screenshot screenshot)
        {
            ScreenshotViewModel model = new ScreenshotViewModel()
            {
                Id = screenshot.Id,
                Title = screenshot.Title,
                Description = screenshot.Description,
                ImagePath = screenshot.ImagePath,
                GameId = screenshot.GameId,
                Game = null,
                IsActive = screenshot.IsActive
            };

            return model;
        }

        private List<ScreenshotViewModel> ScreenshotsToScreenshotViewModels(List<Screenshot> Screenshots)
        {
            List<ScreenshotViewModel> model = new List<ScreenshotViewModel>();
            foreach (Screenshot screenshot in Screenshots)
            {
                model.Add(ScreenshotToScreenshotViewModel(screenshot));
            }

            return model;
        }

        private ScreenshotViewModel ScreenshotToScreenshotViewModelIncludeAll(Screenshot screenshot)
        {
            ScreenshotViewModel model = new ScreenshotViewModel()
            {
                Id = screenshot.Id,
                Title = screenshot.Title,
                Description = screenshot.Description,
                ImagePath = screenshot.ImagePath,
                GameId = screenshot.GameId,
                Game = GameToGameViewModel(screenshot.Game),
                IsActive = screenshot.IsActive
            };

            return model;
        }

        private List<ScreenshotViewModel> ScreenshotsToScreenshotViewModelsIncludeAll(List<Screenshot> Screenshots)
        {
            List<ScreenshotViewModel> model = new List<ScreenshotViewModel>();
            foreach (Screenshot screenshot in Screenshots)
            {
                model.Add(ScreenshotToScreenshotViewModelIncludeAll(screenshot));
            }

            return model;
        }

        #endregion Screenshot

        #region Download Link

        private async Task<DownloadLink> GetDownloadLinkAsync(int id)
        {
            return await context.DownloadLinks.FirstOrDefaultAsync(x => x.Id == id);
        }

        private DownloadLink DownloadLinkViewModelToDownloadLink(DownloadLinkViewModel downloadLink)
        {
            DownloadLink model = new DownloadLink()
            {
                Id = downloadLink.Id,
                Title = downloadLink.Title,
                Link = downloadLink.Link,
                GameId = downloadLink.GameId,
                Game = null,
                IsActive = downloadLink.IsActive
            };

            return model;
        }

        private List<DownloadLink> DownloadLinkViewModelsToDownloadLinks(List<DownloadLinkViewModel> downloadLinks)
        {
            List<DownloadLink> model = new List<DownloadLink>();
            foreach (DownloadLinkViewModel downloadLink in downloadLinks)
            {
                model.Add(DownloadLinkViewModelToDownloadLink(downloadLink));
            }

            return model;
        }

        private DownloadLink DownloadLinkViewModelToDownloadLinkIncludeAll(DownloadLinkViewModel downloadLink)
        {
            DownloadLink model = new DownloadLink()
            {
                Id = downloadLink.Id,
                Title = downloadLink.Title,
                Link = downloadLink.Link,
                GameId = downloadLink.GameId,
                Game = GameViewModelToGame(downloadLink.Game),
                IsActive = downloadLink.IsActive
            };

            return model;
        }

        private List<DownloadLink> DownloadLinkViewModelsToDownloadLinksIncludeAll(List<DownloadLinkViewModel> downloadLinks)
        {
            List<DownloadLink> model = new List<DownloadLink>();
            foreach (DownloadLinkViewModel downloadLink in downloadLinks)
            {
                model.Add(DownloadLinkViewModelToDownloadLinkIncludeAll(downloadLink));
            }

            return model;
        }

        private DownloadLinkViewModel DownloadLinkToDownloadLinkViewModel(DownloadLink downloadLink)
        {
            DownloadLinkViewModel model = new DownloadLinkViewModel()
            {
                Id = downloadLink.Id,
                Title = downloadLink.Title,
                Link = downloadLink.Link,
                GameId = downloadLink.GameId,
                Game = null,
                IsActive = downloadLink.IsActive
            };

            return model;
        }

        private List<DownloadLinkViewModel> DownloadLinksToDownloadLinkViewModels(List<DownloadLink> downloadLinks)
        {
            List<DownloadLinkViewModel> model = new List<DownloadLinkViewModel>();
            foreach (DownloadLink downloadLink in downloadLinks)
            {
                model.Add(DownloadLinkToDownloadLinkViewModel(downloadLink));
            }

            return model;
        }

        private DownloadLinkViewModel DownloadLinkToDownloadLinkViewModelIncludeAll(DownloadLink downloadLink)
        {
            DownloadLinkViewModel model = new DownloadLinkViewModel()
            {
                Id = downloadLink.Id,
                Title = downloadLink.Title,
                Link = downloadLink.Link,
                GameId = downloadLink.GameId,
                Game = GameToGameViewModel(downloadLink.Game),
                IsActive = downloadLink.IsActive
            };

            return model;
        }

        private List<DownloadLinkViewModel> DownloadLinksToDownloadLinkViewModelsIncludeAll(List<DownloadLink> downloadLinks)
        {
            List<DownloadLinkViewModel> model = new List<DownloadLinkViewModel>();
            foreach (DownloadLink downloadLink in downloadLinks)
            {
                model.Add(DownloadLinkToDownloadLinkViewModelIncludeAll(downloadLink));
            }

            return model;
        }

        #endregion Download Link

        #region Slider

        private async Task<Slider> GetSliderAsync(int id)
        {
            return await context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<Slider> GetSliderIncludeAllAsync(int id)
        {
            return await context.Sliders.Include(x => x.Game).FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<List<Slider>> GetSlidersAsync()
        {
            return await context.Sliders.ToListAsync();
        }

        private async Task<List<Slider>> GetSlidersIncludeAllAsync()
        {
            return await context.Sliders.Include(x => x.Game).ToListAsync();
        }

        private SliderViewModel SliderToSliderViewModel(Slider slider)
        {
            SliderViewModel model = new SliderViewModel()
            {
                Id = slider.Id,
                SlideImage = slider.SlideImage,
                GameId = slider.GameId,
                Game = null,
                DateOfUpload = slider.DateOfUpload,
                LastUpdatedOn = slider.LastUpdatedOn,
                Order = slider.Order,
                IsActive = slider.IsActive
            };

            return model;
        }

        private List<SliderViewModel> SlidersToSliderViewModels(List<Slider> sliders)
        {
            List<SliderViewModel> model = new List<SliderViewModel>();
            foreach (Slider slider in sliders)
            {
                model.Add(SliderToSliderViewModel(slider));
            }

            return model;
        }

        private SliderViewModel SliderToSliderViewModelIncludeAll(Slider slider)
        {
            SliderViewModel model = new SliderViewModel()
            {
                Id = slider.Id,
                SlideImage = slider.SlideImage,
                GameId = slider.GameId,
                Game = GameToGameViewModel(slider.Game),
                DateOfUpload = slider.DateOfUpload,
                LastUpdatedOn = slider.LastUpdatedOn,
                Order = slider.Order,
                IsActive = slider.IsActive
            };

            return model;
        }

        private List<SliderViewModel> SlidersToSliderViewModelsIncludeAll(List<Slider> sliders)
        {
            List<SliderViewModel> model = new List<SliderViewModel>();
            foreach (Slider slider in sliders)
            {
                model.Add(SliderToSliderViewModelIncludeAll(slider));
            }

            return model;
        }

        private Slider SliderViewModelToSlider(SliderViewModel slider)
        {
            Slider model = new Slider()
            {
                Id = slider.Id,
                SlideImage = slider.SlideImage,
                GameId = slider.GameId,
                Game = null,
                DateOfUpload = slider.DateOfUpload,
                LastUpdatedOn = slider.LastUpdatedOn,
                Order = slider.Order,
                IsActive = slider.IsActive
            };

            return model;
        }

        private List<Slider> SliderViewModelsToSliders(List<SliderViewModel> sliders)
        {
            List<Slider> model = new List<Slider>();
            foreach (SliderViewModel slider in sliders)
            {
                model.Add(SliderViewModelToSlider(slider));
            }

            return model;
        }

        private Slider SliderViewModelToSliderIncludeAll(SliderViewModel slider)
        {
            Slider model = new Slider()
            {
                Id = slider.Id,
                SlideImage = slider.SlideImage,
                GameId = slider.GameId,
                Game = GameViewModelToGame(slider.Game),
                DateOfUpload = slider.DateOfUpload,
                LastUpdatedOn = slider.LastUpdatedOn,
                Order = slider.Order,
                IsActive = slider.IsActive
            };

            return model;
        }

        private List<Slider> SliderViewModelsToSlidersIncludeAll(List<SliderViewModel> sliders)
        {
            List<Slider> model = new List<Slider>();
            foreach (SliderViewModel slider in sliders)
            {
                model.Add(SliderViewModelToSliderIncludeAll(slider));
            }

            return model;
        }

        #endregion Slider

        #region Helper Method

        private DownloadLink DownloadLinkStringToDownloadLink(string downloadLink, string title)
        {
            if (!string.IsNullOrEmpty(downloadLink))
            {
                return new DownloadLink()
                {
                    Id = 0,
                    Title = title,
                    Link = downloadLink,
                    IsActive = true
                };
            }

            return null;
        }

        private List<DownloadLink> DownloadLinksStringToDownloadLinks(string downloadLinkString, string title)
        {
            downloadLinkString = RemoveAllWhiteSpace(downloadLinkString);
            List<string> downloadLinkListString = SplitByComma(downloadLinkString);
            List<DownloadLink> downloadLinkList = new List<DownloadLink>();
            foreach (string downloadLink in downloadLinkListString)
            {
                DownloadLink tempDownloadLink = DownloadLinkStringToDownloadLink(downloadLink, title);
                if (tempDownloadLink != null)
                {
                    downloadLinkList.Add(tempDownloadLink);
                }
            }

            return downloadLinkList;
        }

        private Screenshot HttpPostedFileBaseToScreenshot(HttpPostedFileBase screenshot, string title, string path, string serverPath)
        {
            string filePath = SaveImageFtp(screenshot, path, serverPath);
            if (filePath != null)
            {
                return new Screenshot()
                {
                    Id = 0,
                    Title = title,
                    Description = null,
                    ImagePath = filePath,
                    IsActive = true
                };
            }
            return null;
        }

        private List<Screenshot> HttpPostedFileBaseArrayToScreenshots(HttpPostedFileBase[] screenshots, string title, string path, string serverPath)
        {
            if (screenshots.Count() > 0)
            {
                List<Screenshot> screenshotsList = new List<Screenshot>();
                foreach (HttpPostedFileBase screenshot in screenshots)
                {
                    Screenshot tempScreenshot = HttpPostedFileBaseToScreenshot(screenshot, title, path, serverPath);
                    if (tempScreenshot != null)
                    {
                        screenshotsList.Add(tempScreenshot);
                    }
                }
                return screenshotsList;
            }
            return null;
        }

        private string GetRandomAlphaString(int? length = 35)
        {
            char[] letters = "QWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray();
            Random random = new Random();
            StringBuilder randomString = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                randomString.Append(letters[random.Next(0, 36)]);
            }
            return randomString.ToString();
        }

        private string GetRandomNumericString(int? length = 35)
        {
            char[] letters = "1234567890".ToCharArray();
            Random random = new Random();
            StringBuilder randomString = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                randomString.Append(letters[random.Next(0, 36)]);
            }
            return randomString.ToString();
        }

        private string GetRandomAlphaNumericString(int? length = 35)
        {
            char[] letters = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890".ToCharArray();
            Random random = new Random();
            StringBuilder randomString = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                randomString.Append(letters[random.Next(0, 36)]);
            }
            return randomString.ToString();
        }

        private string SaveImageFtp(HttpPostedFileBase image, string path, string serverPath)
        {
            if (image != null)
            {
                if (image.ContentLength > 0 && image.ContentType.Contains("image"))
                {
                    string tempPath = path.Replace("~/", "");
                    string fileExtension = Path.GetExtension(image.FileName);
                    string fileName = GetRandomAlphaNumericString() + fileExtension;
                    string fullPath = Path.Combine(serverPath + tempPath, fileName);
                    try
                    {
                        image.SaveAs(fullPath);
                        Thread.Sleep(100);
                        return Path.Combine(path, fileName);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        private List<string> SaveAllImagesFtp(HttpPostedFileBase[] images, string path, string serverPath)
        {
            if (images.Count() > 0)
            {
                List<string> filePaths = new List<string>();
                foreach (HttpPostedFileBase image in images)
                {
                    string filePath = SaveImageFtp(image, path, serverPath);
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        filePaths.Add(filePath);
                    }
                }
                return filePaths;
            }
            return null;
        }

        private void RemoveImageFtp(string imagePath, string serverPath)
        {
            if (imagePath != null)
            {
                imagePath = imagePath.Replace("~/", "");
                FileInfo image = new FileInfo(Path.Combine(serverPath, imagePath));
                if (image.Exists)
                {
                    image.Delete();
                }
            }
        }

        private void RemoveAllImagesFtp(List<string> imagesPath, string serverPath)
        {
            if (imagesPath.Count() > 0)
            {
                foreach (string ImagePath in imagesPath)
                {
                    RemoveImageFtp(ImagePath, serverPath);
                }
            }
        }

        private string RemoveAllWhiteSpace(string str)
        {
            return new string(str.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
        }

        private List<string> SplitByComma(string str)
        {
            return str.Split(',').ToList();
        }

        #endregion Helper Method
    }

    // Contains Public Methods for Admin
    public partial class GamesGalleryRepository 
    {
        #region Game

        public async Task<List<GameViewModel>> GamesAsync()
        {
            return GamesToGameViewModels(await GetGamesAsync());
        }

        public async Task<List<GameViewModel>> GamesIncludeAllAsync()
        {
            return GamesToGameViewModelsIncludeAll(await GetGamesIncludeAllAsync());
        }

        public async Task<GameViewModel> GameAsync(int id)
        {
            Game game = await GetGameAsync(id);
            if (game == null)
            {
                return null;
            }

            return GameToGameViewModel(game);
        }

        public async Task<GameViewModel> GameIncludeAllAsync(int id)
        {
            Game game = await GetGameIncludeAllAsync(id);
            if (game == null)
            {
                return null;
            }

            return GameToGameViewModelIncludeAll(game);
        }

        public async Task<bool> AddGameAsync(GameViewModel model)
        {
            Game game = GameViewModelToGame(model);
            game.Id = 0;
            game.CoverImage = SaveImageFtp(model.CoverImageHelper, model.Path, model.ServerPath);
            game.DateOfUpload = DateTime.Now;
            game.DownloadLinks = DownloadLinksStringToDownloadLinks(model.DownloadLink, model.Title);
            game.Screenshots = HttpPostedFileBaseArrayToScreenshots(model.Screenshot, model.Title, model.Path, model.ServerPath);
            game.Categories = await GetCategoriesById(model.CategoriesId);
            game.LastUpdatedOn = DateTime.Now;

            context.Games.Add(game);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditGameAsync(GameViewModel model)
        {
            Game game = await GetGameIncludeAllAsync(model.Id);
            if (model.CoverImage != null)
            {
                if (game.CoverImage != null)
                {
                    RemoveImageFtp(game.CoverImage, model.ServerPath);
                }
                game.CoverImage = SaveImageFtp(model.CoverImageHelper, model.Path, model.ServerPath);
            }
            if (model.DownloadLink != null)
            {
                if (model.DownloadLinkInsertionMode == "Replace")
                {
                    List<DownloadLink> downloadLinks = game.DownloadLinks.ToList();
                    foreach (DownloadLink downloadLink in downloadLinks)
                    {
                        game.DownloadLinks.Remove(downloadLink);
                        context.DownloadLinks.Remove(downloadLink);
                    }
                }
                string downloadLinkString = RemoveAllWhiteSpace(model.DownloadLink);
                List<string> downloadLinkListString = SplitByComma(downloadLinkString);
                foreach (string downloadLink in downloadLinkListString)
                {
                    game.DownloadLinks.Add(DownloadLinkStringToDownloadLink(downloadLink, model.Title));
                }
            }
            if (model.Screenshot[0] != null)
            {
                if (model.ScreenshotInsertionMode == "Replace")
                {
                    List<Screenshot> screenshots = game.Screenshots.ToList();
                    foreach (Screenshot screenshot in screenshots)
                    {
                        RemoveImageFtp(screenshot.ImagePath, model.ServerPath);
                        game.Screenshots.Remove(screenshot);
                        context.Screenshots.Remove(screenshot);
                    }
                }
                foreach (HttpPostedFileBase screenshot in model.Screenshot)
                {
                    game.Screenshots.Add(HttpPostedFileBaseToScreenshot(screenshot, model.Title, model.Path, model.ServerPath));
                }
            }
            if (model.CategoriesId != null)
            {
                if (model.CategoriesInsertionMode == "Replace")
                {
                    List<Category> categories = game.Categories.ToList();
                    foreach (Category category in categories)
                    {
                        game.Categories.Remove(category);
                    }
                }
                List<Category> Categories = context.Categories.ToList();
                foreach (int categoryId in model.CategoriesId)
                {
                    Category category = Categories.Where(x => x.Id == categoryId).FirstOrDefault();
                    if (category != null)
                    {
                        game.Categories.Add(category);
                    }
                }
            }
            game.Title = model.Title;
            game.Description = model.Description;
            game.Size = model.Size;
            game.MinimumRequirements = model.MinimumRequirements;
            game.RecommendedRequirements = model.RecommendedRequirements;
            game.VideoTutorial = model.VideoTutorial;
            game.TotalDownloads = model.TotalDownloads > -1 ? model.TotalDownloads : game.TotalDownloads;
            game.YearOfRelease = model.YearOfRelease <= DateTime.Now.Year && model.YearOfRelease >= DateTime.Now.AddYears(-25).Year ? model.YearOfRelease : game.YearOfRelease;
            game.LastUpdatedOn = DateTime.Now;
            game.IsActive = model.IsActive;

            context.Entry(game).State = System.Data.Entity.EntityState.Modified;
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveGameAsync(int id, string serverPath)
        {
            Game game = await GetGameIncludeAllAsync(id);
            if (game == null)
            {
                return false;
            }
            RemoveImageFtp(game.CoverImage, serverPath);
            await RemoveAllScreenshotsAsync(game.Screenshots.ToList(), serverPath);
            await RemoveAllDownloadLinksAsync(game.DownloadLinks.ToList());
            List<Slider> sliders = await SlidersListByGameIdAsync(game.Id);
            if (sliders.Count() > 0)
            {
                foreach (Slider slider in sliders)
                {
                    RemoveImageFtp(slider.SlideImage, serverPath);
                    context.Sliders.Remove(slider);
                }
            }
            context.Games.Remove(game);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveSelectedGamesAsync(List<int> GamesId, string serverPath)
        {
            List<Game> games = await context.Games.Include(x => x.DownloadLinks).Include(x => x.Screenshots).Include(x => x.Categories).Where(x => GamesId.Contains(x.Id)).ToListAsync();
            List<Slider> sliders = await GetSlidersAsync();
            foreach (Game game in games)
            {
                RemoveImageFtp(game.CoverImage, serverPath);
                await RemoveAllScreenshotsAsync(game.Screenshots.ToList(), serverPath);
                await RemoveAllDownloadLinksAsync(game.DownloadLinks.ToList());
                List<Slider> gameSliders = sliders.Where(x => x.GameId == game.Id).ToList();
                if (gameSliders.Count() > 0)
                {
                    foreach (Slider slider in gameSliders)
                    {
                        RemoveImageFtp(slider.SlideImage, serverPath);
                        context.Sliders.Remove(slider);
                    }
                }

                context.Games.Remove(game);
            }
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<List<SelectListItem>> GamesAsListOfSelectListItemAsync()
        {
            List<SelectListItem> list = await context.Games.Where(x => x.IsActive == true).Select(
            x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToListAsync();

            return list;
        }

        #endregion Game

        #region Category

        public async Task<List<CategoryViewModel>> CategoriesAsync()
        {
            return CategoriesToCategoryViewModels(await GetCategoriesAsync());
        }

        public async Task<List<CategoryViewModel>> CategoriesIncludeAllAsync()
        {
            return CategoriesToCategoryViewModelsIncludeAll(await GetCategoriesIncludeAllAsync());
        }

        public async Task<CategoryViewModel> CategoryIncludeAllAsync(int id)
        {
            Category category = await GetCategoryIncludeAllAsync(id);
            if (category == null)
            {
                return null;
            }

            return CategoryToCategoryViewModelIncludeAll(category);
        }

        public async Task<bool> AddCategoryAsync(CategoryViewModel model)
        {
            Category category = CategoryViewModelToCategory(model);

            context.Categories.Add(category);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<CategoryViewModel> AddCategoryByTitleAsync(string title)
        {
            title = Regex.Replace(title, @"\s+", " ");
            title = title.ToLower();
            title = char.ToUpper(title[0]) + title.Substring(1);
            if (await GetCategoryStatusByTitle(title))
            {
                return null;
            }
            Category category = new Category()
            {
                Id = 0,
                Title = title,
                Description = null,
                Games = null,
                IsActive = true
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            if (category.Id == 0)
            {
                return null;
            }

            return CategoryToCategoryViewModel(category);
        }

        public async Task<bool> EditCategoryAsync(CategoryViewModel model)
        {
            Category category = await GetCategoryIncludeAllAsync(model.Id);
            category.Title = model.Title;
            category.Description = model.Description;
            category.IsActive = model.IsActive;

            context.Entry(category).State = System.Data.Entity.EntityState.Modified;
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveCategoryAsync(int id)
        {
            Category category = await GetCategoryAsync(id);
            if (category == null)
            {
                return false;
            }
            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveSelectedCategoryAsync(List<int> CategoriesId)
        {
            List<Category> categories = await GetCategoriesById(CategoriesId);
            foreach (Category category in categories)
            {
                context.Categories.Remove(category);
            }
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<List<SelectListItem>> CategoriesAsListOfSelectListItemAsync()
        {
            List<SelectListItem> list = await context.Categories.Where(x => x.IsActive == true).Select(
            x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToListAsync();

            return list;
        }

        #endregion Category

        #region Screenshot

        public async Task<List<ScreenshotViewModel>> ScreenshotsListByGameIdAsync(int gameId)
        {
            List<Screenshot> screenshots = await GetScreenshotsByGameIdAsync(gameId);
            if (screenshots == null)
            {
                return null;
            }

            return ScreenshotsToScreenshotViewModels(screenshots.ToList());
        }

        public async Task<ScreenshotViewModel> ScreenshotDetailsIncludeAllAsync(int id)
        {
            Screenshot screenshot = await GetScreenshotIncludeAllAsync(id);
            if (screenshot == null)
            {
                return null;
            }

            return ScreenshotToScreenshotViewModelIncludeAll(screenshot);
        }

        public async Task<bool> AddScreenshotAsync(ScreenshotViewModel model)
        {
            Game game = await GetGameIncludeAllAsync(model.GameId);
            if (game == null)
            {
                return false;
            }
            string filePath = SaveImageFtp(model.ImageHelper, model.Path, model.ServerPath);
            if (filePath == null)
            {
                return false;
            }
            Screenshot screenshot = ScreenshotViewModelToScreenshot(model);
            screenshot.Id = 0;
            screenshot.ImagePath = filePath;
            game.Screenshots.Add(screenshot);
            context.Screenshots.Add(screenshot);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditScreenshotAsync(ScreenshotViewModel model)
        {
            Screenshot screenshot = await GetScreenshotIncludeAllAsync(model.Id);
            if (model.ImageHelper.ContentLength > 0)
            {
                string filePath = SaveImageFtp(model.ImageHelper, model.Path, model.ServerPath);
                if (filePath != null)
                {
                    if (screenshot.ImagePath != null)
                    {
                        RemoveImageFtp(screenshot.ImagePath, model.ServerPath);
                    }
                    screenshot.ImagePath = filePath;
                }
            }
            screenshot.Title = model.Title;
            screenshot.Description = model.Description;
            screenshot.IsActive = model.IsActive;

            context.Entry(screenshot).State = System.Data.Entity.EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveScreenshotAsync(int id, string serverPath)
        {
            Screenshot screenshot = await GetScreenshotAsync(id);
            if (screenshot == null)
            {
                return false;
            }
            RemoveImageFtp(screenshot.ImagePath, serverPath);
            context.Screenshots.Remove(screenshot);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveSelectedScreenshotAsync(List<int> ScreenshotsId, string serverPath)
        {
            List<Screenshot> screenshots = await context.Screenshots.Where(x => ScreenshotsId.Contains(x.Id)).ToListAsync();
            foreach (Screenshot screenshot in screenshots)
            {
                RemoveImageFtp(screenshot.ImagePath, serverPath);
                context.Screenshots.Remove(screenshot);
            }
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveAllScreenshotsAsync(List<Screenshot> screenshots, string serverPath)
        {
            foreach (Screenshot screenshot in screenshots)
            {
                await RemoveScreenshotAsync(screenshot.Id, serverPath);
            }

            return true;
        }

        #endregion Screenshot

        #region Download Link

        public async Task<bool> RemoveDownloadLinkAsync(int id)
        {
            DownloadLink downloadLink = await GetDownloadLinkAsync(id);
            if (downloadLink == null)
            {
                return false;
            }
            context.DownloadLinks.Remove(downloadLink);
            return true;
        }

        public async Task<bool> RemoveAllDownloadLinksAsync(List<DownloadLink> downloadLinks)
        {
            foreach (DownloadLink downloadLink in downloadLinks)
            {
                await RemoveDownloadLinkAsync(downloadLink.Id);
            }

            return true;
        }

        #endregion DownloadLink

        #region Slider

        public async Task<List<SliderViewModel>> SlidersAsync()
        {
            return SlidersToSliderViewModels(await GetSlidersAsync());
        }

        public async Task<List<SliderViewModel>> SlidersIncludeAllAsync()
        {
            return SlidersToSliderViewModelsIncludeAll(await GetSlidersIncludeAllAsync());
        }

        public async Task<SliderViewModel> SliderAsync(int id)
        {
            Slider slider = await GetSliderAsync(id);
            if (slider == null)
            {
                return null;
            }

            return SliderToSliderViewModel(slider);
        }

        public async Task<SliderViewModel> SliderIncludeAllAsync(int id)
        {
            Slider slider = await GetSliderIncludeAllAsync(id);
            if (slider == null)
            {
                return null;
            }

            return SliderToSliderViewModelIncludeAll(slider);
        }

        public async Task<List<Slider>> SlidersListByGameIdAsync(int GameId)
        {
            return await context.Sliders.Where(x => x.GameId == GameId).ToListAsync();
        }

        public async Task<List<Slider>> SlidersListByGameIdIncludeAllAsync(int GameId)
        {
            return await context.Sliders.Include(x => x.Game).Where(x => x.GameId == GameId).ToListAsync();
        }

        public async Task<bool> AddSliderAsync(SliderViewModel model)
        {
            Slider slider = new Slider()
            {
                SlideImage = SaveImageFtp(model.SlideImageHelper, model.Path, model.ServerPath),
                GameId = model.GameId,
                Game = await GetGameAsync(model.GameId),
                DateOfUpload = DateTime.Now,
                LastUpdatedOn = DateTime.Now,
                Order = model.Order,
                IsActive = model.IsActive
            };

            context.Sliders.Add(slider);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditSliderAsync(SliderViewModel model)
        {
            Slider slider = await GetSliderIncludeAllAsync(model.Id);
            if (model.SlideImage != null)
            {
                if (slider.SlideImage != null)
                {
                    RemoveImageFtp(slider.SlideImage, model.ServerPath);
                }
                slider.SlideImage = SaveImageFtp(model.SlideImageHelper, model.Path, model.ServerPath);
            }
            slider.Game = await GetGameAsync(model.GameId);
            slider.LastUpdatedOn = DateTime.Now;
            slider.Order = model.Order;
            slider.IsActive = model.IsActive;

            context.Entry(slider).State = System.Data.Entity.EntityState.Modified;
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveSliderAsync(int id, string serverPath)
        {
            Slider slider = await GetSliderIncludeAllAsync(id);
            if (slider == null)
            {
                return false;
            }
            RemoveImageFtp(slider.SlideImage, serverPath);

            context.Sliders.Remove(slider);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveSelectedSlidersAsync(List<int> slidersId, string serverPath)
        {
            List<Slider> sliders = await context.Sliders.Include(x => x.Game).Where(x => slidersId.Contains(x.Id)).ToListAsync();
            foreach (Slider slider in sliders)
            {
                RemoveImageFtp(slider.SlideImage, serverPath);
                context.Sliders.Remove(slider);
            }
            await context.SaveChangesAsync();

            return true;
        }

        #endregion Slider
    }

    //Contains Public Methos for Users
    public partial class GamesGalleryRepository
    {
        #region Game

        public async Task<List<GameViewModel>> GetActiveGamesAsync()
        {
            List<Game> games = await context.Games.OrderByDescending(x => x.DateOfUpload).Where(x => x.IsActive == true).ToListAsync();

            return GamesToGameViewModels(games);
        }

        public async Task<List<GameViewModel>> GetActiveGamesIncludeAllAsync()
        {
            List<Game> games = await context.Games.Include(x => x.Screenshots).Include(x => x.DownloadLinks).Include(x => x.Categories).OrderByDescending(x => x.DateOfUpload).Where(x => x.IsActive == true && x.Categories.All(y => y.IsActive == true)).ToListAsync();

            return GamesToGameViewModelsIncludeAll(games);
        }

        public async Task<List<GameViewModel>> GetActiveGamesByCategoryAsync(string category)
        {
            List<Game> games = await context.Games.OrderByDescending(x => x.DateOfUpload).Where(x => x.IsActive == true && x.Categories.Any(y => y.Title.ToLower() == category.ToLower())).ToListAsync();

            return GamesToGameViewModels(games);
        }

        public async Task<List<GameViewModel>> GetActiveGamesByCategoryIncludeAllAsync(string category)
        {
            List<Game> games = await context.Games.Include(x => x.Screenshots).Include(x => x.DownloadLinks).Include(x => x.Categories).OrderByDescending(x => x.DateOfUpload).Where(x => x.IsActive == true && x.Categories.Any(y => y.Title.ToLower() == category.ToLower()) && x.Categories.All(y => y.IsActive == true)).ToListAsync();

            return GamesToGameViewModelsIncludeAll(games);
        }

        public async Task<List<GameViewModel>> GetActiveGamesByTitleAsync(string title)
        {
            List<Game> games = await context.Games.OrderByDescending(x => x.DateOfUpload).Where(x => x.IsActive == true && x.Title.ToLower().Contains(title.ToLower())).ToListAsync();

            return GamesToGameViewModels(games);
        }

        public async Task<List<GameViewModel>> GetActiveGamesByTitleIncludeAllAsync(string title)
        {
            List<Game> games = await context.Games.Include(x => x.Screenshots).Include(x => x.DownloadLinks).Include(x => x.Categories).OrderByDescending(x => x.DateOfUpload).Where(x => x.IsActive == true && x.Title.ToLower().Contains(title.ToLower()) && x.Categories.All(y => y.IsActive == true)).ToListAsync();

            return GamesToGameViewModelsIncludeAll(games);
        }

        public async Task<bool> GameDownloadedAsync(int id)
        {
            Game game = await GetGameIncludeAllAsync(id);
            if(game != null && game.IsActive == true)
            {
                game.TotalDownloads++;
                context.Entry(game).State = System.Data.Entity.EntityState.Modified;
                await context.SaveChangesAsync();

                return true;
            }
            return false;
        }

        #endregion Game

        #region Category

        public List<string> GetActiveCategoriesAsList()
        {
            return context.Categories.Where(x => x.IsActive == true && x.Games.Any(y => y.IsActive == true)).OrderBy(x => x.Title).Select(x => x.Title).ToList();
        }

        #endregion Category

        #region Slider

        public async Task<List<SliderViewModel>> GetActiveSlidersAsync()
        {
            List<Slider> sliders = await context.Sliders.OrderBy(x => x.Order).Where(x => x.IsActive == true).ToListAsync();

            return SlidersToSliderViewModels(sliders);
        }

        public async Task<List<SliderViewModel>> GetActiveSlidersIncludeAllAsync()
        {
            List<Slider> sliders = await context.Sliders.Include(x => x.Game).OrderBy(x => x.Order).Where(x => x.IsActive == true && x.Game.IsActive == true && x.Game.Categories.All(y => y.IsActive == true)).ToListAsync();

            return SlidersToSliderViewModelsIncludeAll(sliders);
        }

        #endregion Slider
    }
}