using System;
using System.Globalization;
using Xamarin.Forms;

namespace Advocates.Converters
{
    public class DateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dto = (DateTimeOffset)value;
            DateTime utc = dto.UtcDateTime;

            return utc.ToString("t", CultureInfo.CreateSpecificCulture("en-US"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
