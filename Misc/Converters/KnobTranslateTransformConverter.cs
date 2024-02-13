using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace YetAnotherMessenger.Misc.Converters
{
    internal class KnobTranslateTransformConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
        {
            double result = 1.0;

            foreach (var value in values)
            {
                switch (value)
                {
                    case double num:
                        result *= num;
                        break;
                }
            }

            return parameter is not null ? result * System.Convert.ToDouble(parameter, culture) : result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
