using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGallery.DL
{
    public class Slider
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string SlideImage { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public DateTime? DateOfUpload { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; }
    }
}
