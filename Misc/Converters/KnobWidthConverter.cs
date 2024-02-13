using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace YetAnotherMessenger.Misc.Converters
{
    class KnobWidthConverter : IValueConverter
    {
        private readonly double _ratio = 0.5;

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not null)
            {
                return parameter is not null ? (double)value * _ratio - ((Thickness)parameter).Left : (double)value * _ratio;
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
