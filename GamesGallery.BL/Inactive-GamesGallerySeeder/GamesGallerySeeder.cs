using GamesGallery.DL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGallery.BL
{
    public class GamesGallerySeeder : DropCreateDatabaseIfModelChanges<GamesGalleryDbContext>
    {
        protected override void Seed(GamesGalleryDbContext context)
        {
            Category category1 = new Category()
            {
                Id = 1,
                Title = "Shooting",
                Description = null,
                Games = null,
                IsActive = true
            };
            Category category2 = new Category()
            {
                Id = 2,
                Title = "Racing",
                Description = null,
                Games = null,
                IsActive = true
            };
            Category category3 = new Category()
            {
                Id = 3,
                Title = "Survival",
                Description = null,
                Games = null,
                IsActive = true
            };
            Category category4 = new Category()
            {
                Id = 4,
                Title = "Adventure",
                Description = null,
                Games = null,
                IsActive = true
            };
            Category category5 = new Category()
            {
                Id = 1,
                Title = "Horror",
                Description = null,
                Games = null,
                IsActive = true
            };

            context.Categories.Add(category1);
            context.Categories.Add(category2);
            context.Categories.Add(category3);
            context.Categories.Add(category4);
            context.Categories.Add(category5);

            base.Seed(context);
        }
    }
}
