using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Mobile.Converters
{
    class RatingAndReviewsToColorConverter : IMultiValueConverter
    {
        public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isBackground = parameter is string param && string.Equals(param, "background", StringComparison.OrdinalIgnoreCase);

            var hex = isBackground ? "#F2F2F2" : "#EBEBEB";

            if (values.Length == 2 && values[0] is int reviewCount && values[1] is double rating)
            {
                if (reviewCount >= 15)
                {
                    hex = rating switch
                    {
                        double r when r > 0 && r < 1.4 => isBackground ? "#E0F7FA" : "#ADD8E6",
                        double r when r < 2.4 => isBackground ? "#F0C085" : "#CD7F32",
                        double r when r < 3.5 => isBackground ? "#E5E5E5" : "#C0C0C0",
                        double r when r <= 4.0 => isBackground ? "#FFF9D6" : "#FFD700",
                        _ => null
                    };
                }
            }
            return hex is null ? null : Color.FromArgb(hex);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
