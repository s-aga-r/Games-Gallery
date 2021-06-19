using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GamesGallery.ViewModel.Helper
{
    public class GamesGalleryLogger : ActionFilterAttribute, IExceptionFilter
    {
        private string filePath;
        private StringBuilder stringBuilder;

        // Public Constructor
        public GamesGalleryLogger()
        {
            filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Logs/"), DateTime.Now.ToShortDateString() + ".txt");
            stringBuilder = new StringBuilder();
        }

        private void Logger(string data)
        {
            File.AppendAllText(filePath, data);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            stringBuilder.Append("\n=====================================================================================================================================");
            stringBuilder.Append("\n" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "\t\t-->\t\t" + filterContext.ActionDescriptor.ActionName + "\t\t-->\t\tOnActionExecuting\t\t-->\t\t@" + DateTime.Now.ToString());
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            stringBuilder.Append("\n" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "\t\t-->\t\t" + filterContext.ActionDescriptor.ActionName + "\t\t-->\t\tOnActionExecuted\t\t-->\t\t@" + DateTime.Now.ToString());
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            stringBuilder.Append("\n" + filterContext.RouteData.Values["controller"].ToString() + "\t\t-->\t\t" + filterContext.RouteData.Values["action"].ToString() + "\t\t-->\t\tOnResultExecuting\t\t-->\t\t@" + DateTime.Now.ToString());
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            stringBuilder.Append("\n" + filterContext.RouteData.Values["controller"].ToString() + "\t\t-->\t\t" + filterContext.RouteData.Values["action"].ToString() + "\t\t-->\t\tOnResultExecuted\t\t-->\t\t@" + DateTime.Now.ToString());
            Logger(stringBuilder.ToString());
            stringBuilder.Clear();
        }

        public void OnException(ExceptionContext filterContext)
        {
            stringBuilder.Append("\n\n\n\n\n\n#####################################################################################################################################");
            stringBuilder.Append("\n# " + filterContext.RouteData.Values["controller"].ToString() + "\t\t-->\t\t" + filterContext.RouteData.Values["action"].ToString() + "\t\t-->\t\t" + filterContext.Exception.Message + "\t\t-->\t\t@" + DateTime.Now.ToString());
            stringBuilder.Append("\n#####################################################################################################################################\n\n\n\n\n");
            Logger(stringBuilder.ToString());
            stringBuilder.Clear();
        }
    }
}