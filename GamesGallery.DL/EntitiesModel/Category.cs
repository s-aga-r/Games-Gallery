using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGallery.DL
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual IList<Game> Games { get; set; }

        public bool IsActive { get; set; }
    }
}
