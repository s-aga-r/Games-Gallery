using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GamesGallery.ViewModel
{
    public class SliderViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Slide Image*")]
        public string SlideImage { get; set; }

        public HttpPostedFileBase SlideImageHelper { get; set; }
        public string Path { get; set; }
        public string ServerPath { get; set; }

        [Display(Name = "Game*")]
        public int GameId { get; set; }

        public List<SelectListItem> GamesList { get; set; }

        public GameViewModel Game { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Upload")]
        public DateTime? DateOfUpload { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Last Updated On")]
        public DateTime? LastUpdatedOn { get; set; }

        [Display(Name = "Order*")]
        [Required(ErrorMessage = "This field is required.")]
        public int Order { get; set; }

        [Display(Name = "Active Status")]
        public bool IsActive { get; set; }

        public SliderViewModel()
        {
            this.Id = 0;
            this.SlideImage = null;
            this.GameId = 0;
            this.Game = null;
            this.DateOfUpload = DateTime.Now;
            this.LastUpdatedOn = DateTime.Now;
            this.Order = 0;
            this.IsActive = true;
        }
    }
}
