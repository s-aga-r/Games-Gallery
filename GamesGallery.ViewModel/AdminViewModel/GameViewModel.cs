using GamesGallery.ViewModel.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GamesGallery.ViewModel
{
    public class GameViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Title*")]
        [Required(ErrorMessage = "This field is required.")]
        public string Title { get; set; }

        [MinLength(50)]
        [Display(Name = "Description*")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "This field is required.")]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Cover Image*")]
        public string CoverImage { get; set; }

        public HttpPostedFileBase CoverImageHelper { get; set; }
        public string Path { get; set; }
        public string ServerPath { get; set; }

        [Display(Name = "Size in GB*")]
        [Required(ErrorMessage = "This field is required.")]
        [Range(0, float.MaxValue, ErrorMessage = "The field Size must be greater than 0.")]
        public float Size { get; set; }

        [Display(Name = "Total Downloads")]
        public int TotalDownloads { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Minimum Requirements*")]
        [Required(ErrorMessage = "This field is required.")]
        public string MinimumRequirements { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Recommended Requirements*")]
        [Required(ErrorMessage = "This field is required.")]
        public string RecommendedRequirements { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Video Tutorial")]
        [DisplayFormat(NullDisplayText = "-")]
        public string VideoTutorial { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Upload")]
        public DateTime? DateOfUpload { get; set; }

        [Display(Name = "Year of Release*")]
        [YearOfReleaseValidation]
        [Required(ErrorMessage = "This field is required.")]
        public int YearOfRelease { get; set; }

        [NotMapped]
        [Display(Name = "Download Links*")]
        public string DownloadLink { get; set; }

        public string DownloadLinkInsertionMode { get; set; }

        [Display(Name = "Download Links*")]
        [DisplayFormat(NullDisplayText = "-")]
        public IList<DownloadLinkViewModel> DownloadLinks { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase[] Screenshot { get; set; }

        public string ScreenshotInsertionMode { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public IList<ScreenshotViewModel> Screenshots { get; set; }

        [NotMapped]
        [MinLength(2)]
        [MaxLength(50)]
        [Display(Name = "Add Category")]
        [Remote("IsCategoryAvailable", "Games", ErrorMessage = "Category already in the list.")]
        public string AddCategory { get; set; }

        [NotMapped]
        [Display(Name = "Categories*")]
        public List<int> CategoriesId { get; set; }

        public string CategoriesInsertionMode { get; set; }

        public List<SelectListItem> CategoriesList { get; set; }

        [Display(Name = "Categories*")]
        [DisplayFormat(NullDisplayText = "-")]
        public virtual IList<CategoryViewModel> Categories { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Last Updated On")]
        public DateTime? LastUpdatedOn { get; set; }

        [Display(Name = "Active Status")]
        public bool IsActive { get; set; }

        public GameViewModel()
        {
            this.Id = 0;
            this.Title = null;
            this.Description = null;
            this.CoverImage = null;
            this.Size = 0;
            this.TotalDownloads = 0;
            this.MinimumRequirements = "Operating System : Windows 7\nProcessor : Intel i3 3rd Gen\nRAM : 2GB\nFree HardDisk Space : 10GB";
            this.RecommendedRequirements = "Operating System : Windows 8.1 or Above\nProcessor : Intel i5 3rd Gen or Above\nRAM : 4GB or Above\nFree HardDisk Space : 10GB or Above";
            this.VideoTutorial = null;
            this.DateOfUpload = DateTime.Now;
            this.YearOfRelease = DateTime.Now.Year;
            this.DownloadLinks = null;
            this.Screenshots = null;
            this.LastUpdatedOn = DateTime.Now;
            this.IsActive = true;
        }
    }
}
