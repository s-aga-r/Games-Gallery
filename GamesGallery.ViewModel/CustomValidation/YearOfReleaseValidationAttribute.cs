using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GamesGallery.ViewModel.CustomValidation
{
    public class YearOfReleaseValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int YearOfRelease = (int)value;
            if (YearOfRelease > DateTime.Now.Year || YearOfRelease < DateTime.Now.AddYears(-25).Year)
            {
                ErrorMessage = $"Please enter a value between {DateTime.Now.AddYears(-25).Year} to {DateTime.Now.Year}.";
                return false;
            }
            return true;
        }
    }
}
