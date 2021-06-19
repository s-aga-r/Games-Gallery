using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGallery.ViewModel.Helper
{
    public class DateToTimeAgo
    {
        public static string GetTimeAgo(DateTime date)
        {
            TimeSpan oSpan = DateTime.Now.Subtract(date);
            double TotalMinutes = oSpan.TotalMinutes;
            string Suffix = " ago";

            var aValue = new SortedList<double, Func<string>>();
            aValue.Add(0.75, () => "less than a minute");
            aValue.Add(1.5, () => "1 minute");
            aValue.Add(45, () => string.Format("{0} minutes", Math.Round(TotalMinutes)));
            aValue.Add(90, () => "1 hour");
            aValue.Add(1440, () => string.Format("{0} hours", Math.Round(Math.Abs(oSpan.TotalHours)))); // 60 * 24
            aValue.Add(2880, () => "1 day"); // 60 * 48
            aValue.Add(43200, () => string.Format("{0} days", Math.Floor(Math.Abs(oSpan.TotalDays)))); // 60 * 24 * 30
            aValue.Add(86400, () => "1 month"); // 60 * 24 * 60
            aValue.Add(525600, () => string.Format("{0} months", Math.Floor(Math.Abs(oSpan.TotalDays / 30)))); // 60 * 24 * 365 
            aValue.Add(1051200, () => "1 year"); // 60 * 24 * 365 * 2
            aValue.Add(double.MaxValue, () => string.Format("{0} years", Math.Floor(Math.Abs(oSpan.TotalDays / 365))));

            return aValue.First(n => TotalMinutes < n.Key).Value.Invoke() + Suffix;
        }
    }
}
