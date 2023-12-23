using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Mobile.Converters
{
    public class RatingToStarsConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not double rating || rating < 0 || rating > 4)
            {
                return string.Empty;
            }
            string fullStar = "\ue838";
            string halfStar = "\ue839";
            int fullStars = (int)rating;
            bool hasHalfStar = rating % 1 >= 0.5;

            return string.Concat(string.Join("", Enumerable.Repeat(fullStar,fullStars)), hasHalfStar ? halfStar : "");
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
