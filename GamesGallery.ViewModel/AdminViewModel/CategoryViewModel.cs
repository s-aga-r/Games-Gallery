using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGallery.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [StringLength(50), Display(Name = "Title*")]
        [Required(ErrorMessage = "This field is required.")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual IList<GameViewModel> Games { get; set; }

        [Display(Name = "Active Status")]
        public bool IsActive { get; set; }

        public CategoryViewModel()
        {
            this.Id = 0;
            this.Title = null;
            this.Description = null;
            this.Games = null;
            this.IsActive = true;
        }
    }
}
