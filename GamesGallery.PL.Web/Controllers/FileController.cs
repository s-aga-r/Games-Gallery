using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamesGallery.PL.Web.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public FileResult ShowImage(string imagePath)
        {
            string fullPath = Server.MapPath(imagePath);
            return File(fullPath, "image/jpg");
        }
    }
}