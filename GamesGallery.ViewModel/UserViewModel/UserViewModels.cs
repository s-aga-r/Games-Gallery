using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGallery.ViewModel
{
    public class IndexViewModel
    {
        public IPagedList<GameViewModel> Games { get; set; }
        public List<SliderViewModel> Sliders { get; set; }
    }

    public class GamesCategory
    {
        public IPagedList<GameViewModel> Games { get; set; }
        public CategoryViewModel category { get; set; }
    }
}
