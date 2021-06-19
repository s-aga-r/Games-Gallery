using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GamesGallery.ViewModel
{
    public class ScreenshotViewModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Screenshot*")]
        public string ImagePath { get; set; }

        public HttpPostedFileBase ImageHelper { get; set; }
        public string Path { get; set; }
        public string ServerPath { get; set; }

        [Display(Name = "Active Status")]
        public bool IsActive { get; set; }

        public int GameId { get; set; }

        public GameViewModel Game { get; set; }

        public ScreenshotViewModel()
        {
            this.Id = 0;
            this.Title = null;
            this.Description = null;
            this.ImagePath = null;
            this.IsActive = true;
            this.GameId = 0;
            this.Game = null;
        }
    }
}
