using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGallery.DL
{
    public class DownloadLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public string Link { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public bool IsActive { get; set; }
    }
}
