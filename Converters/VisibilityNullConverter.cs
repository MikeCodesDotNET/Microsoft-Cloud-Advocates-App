using System;
using System.Globalization;
using Advocates.Models;
using Xamarin.Forms;

namespace Advocates.Converters
{
    public class VisibilityNullConverter : IValueConverter
    {
        public VisibilityNullConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            else
            {
                var advocate = (Advocate)value;
                if (advocate.IgniteStops == null || advocate.IgniteStops.Count == 0)
                    return false;
                else
                    return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
