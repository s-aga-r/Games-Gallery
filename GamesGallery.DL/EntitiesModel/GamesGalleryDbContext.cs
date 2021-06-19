using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGallery.DL
{
    public class GamesGalleryDbContext : DbContext
    {
        public GamesGalleryDbContext() : base("EFContext") { }

        public DbSet<Game> Games { get; set; }

        public DbSet<DownloadLink> DownloadLinks { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Screenshot> Screenshots { get; set; }

        public DbSet<Slider> Sliders { get; set; }
    }
}
