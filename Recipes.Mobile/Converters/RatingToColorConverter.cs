using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Mobile.Converters
{
    public class RatingToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            //bool isBackground = parameter is string param && param.ToLower() == "background";
            bool isBackground = parameter is string param && string.Equals(param, "background",StringComparison.OrdinalIgnoreCase);
            var hex = value switch
            {
                double r when r > 0 && r < 1.4 => isBackground ? "#E0F7FA" : "#ADD8E6", //blue
                double r when r < 2.4 => isBackground ? "#F0C085" : "#CD7F32", //bronze
                double r when r < 3.5 => isBackground ? "#E5E5E5" : "#C0C0C0", //silver
                double r when r <= 4 => isBackground ? "#FFF9D6" : "#FFD700", //gold
                _ => "#EBEBEB",
            };
            return Color.FromArgb(hex);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
